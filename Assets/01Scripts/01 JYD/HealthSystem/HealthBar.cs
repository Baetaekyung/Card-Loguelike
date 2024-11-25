using System.Collections;
using CardGame.Players;
using UnityEngine;
using UnityEngine.UI;

namespace CardGame
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private PlayerHealth PlayerHealth;
        [SerializeField] private Image redBarImage;
        [SerializeField] private float lerpSpeed = 15f;
        [SerializeField] private PlayerSingletonSO PlayerSingletonSo;

        private void Start()
        {
        
        }

        private void Update()
        {
            if (PlayerHealth == null)
            {
                PlayerHealth p = PlayerSingletonSo.PlayerTransform.GetComponentInChildren<PlayerHealth>();
                SetPlayerHealth(p);
            }
        }

        private void OnDestroy()
        {
            PlayerHealth.OnHitEvent -= SetHealthBar;
        }

        public void SetPlayerHealth(PlayerHealth _playerHealth)
        {
            PlayerHealth.OnHitEvent += SetHealthBar;
            PlayerHealth.OnHealEvent += SetHealthBar;

            PlayerHealth = _playerHealth;
        }

        

        private void SetHealthBar(ActionData actionData)
        {
            float targetFillAmount = PlayerHealth.GetPercent();
            StartCoroutine(LerpHealthBar(targetFillAmount));
        }
        private void SetHealthBar()
        {
            float targetFillAmount = PlayerHealth.GetPercent();
            StartCoroutine(LerpHealthBar(targetFillAmount));
        }
        

        private IEnumerator LerpHealthBar(float targetFillAmount)
        {
            float startFillAmount = redBarImage.fillAmount;

            float elapsedTime = 0f;
            while (elapsedTime < 1f)
            {
                redBarImage.fillAmount = Mathf.Lerp(startFillAmount, targetFillAmount, elapsedTime);
                elapsedTime += Time.deltaTime * lerpSpeed;
                yield return null;
            }

            redBarImage.fillAmount = targetFillAmount;
        }
    }
}
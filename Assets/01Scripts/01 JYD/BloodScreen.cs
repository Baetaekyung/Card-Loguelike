using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace CardGame
{
    public class BloodScreen : MonoBehaviour
    {
        [SerializeField] private Image bloodScreen;
        [SerializeField] private float duration; // 효과 전체 지속 시간
        [SerializeField] private float maxAlphaValue; // 최대 알파값

        private bool isEffectRunning = false;

        [SerializeField] private PlayerHealth playerHealth;

        private void Start()
        {
            playerHealth.OnHitEvent += BloodScreenStart;
        }
        
        private void OnDestroy()
        {
            playerHealth.OnHitEvent -= BloodScreenStart;
        }


        public void BloodScreenStart(ActionData empty)
        {
            if (!isEffectRunning)
                StartCoroutine(BloodScreenCoroutine());
        }

        private IEnumerator BloodScreenCoroutine()
        {
            isEffectRunning = true;
            float elapsedTime = 0f;

            // 알파값 상승
            while (elapsedTime < duration / 2)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Lerp(0, maxAlphaValue, elapsedTime / (duration / 2));
                SetBloodScreenAlpha(alpha);
                yield return null;
            }

            elapsedTime = 0f;

            // 알파값 하강
            while (elapsedTime < duration / 2)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Lerp(maxAlphaValue, 0, elapsedTime / (duration / 2));
                SetBloodScreenAlpha(alpha);
                yield return null;
            }

            SetBloodScreenAlpha(0);
            isEffectRunning = false;
        }

        private void SetBloodScreenAlpha(float alpha)
        {
            Color color = bloodScreen.color;
            color.a = alpha;
            bloodScreen.color = color;
        }
    }
}
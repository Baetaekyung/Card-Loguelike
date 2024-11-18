using System;
using System.Collections;
using UnityEngine;

namespace CardGame
{
    public class PlayerHealth : MonoBehaviour,IDamageable
    {
        public ActionData ActionData;
        public float MaxHealth => maxHealth;
        public float CurrentHealth => currentHealth;
        public bool IsAlive => isAlive;
        
        [SerializeField] private float maxHealth;
        [SerializeField] private float currentHealth;
        [SerializeField] private bool isAlive;
        [SerializeField] private float camShakePower;
        
        public event Action OnDeadEvent;
        public event Action<ActionData> OnHitEvent;

        private void Start()
        {
            currentHealth = maxHealth;

            OnHitEvent += HitStop;
        }

        public void TakeDamage(ActionData actiondata)
        {
            ActionData.knockBackPower = actiondata.knockBackPower;
            ActionData.damageAmount = actiondata.damageAmount;
            ActionData.hitNormal = actiondata.hitNormal;
            ActionData.hitPoint = actiondata.hitPoint;
            
            OnHitEvent?.Invoke(actiondata);
            
            //before stat system
            currentHealth -= ActionData.damageAmount;
            
            
            if (currentHealth <= 0)
            {
                OnDead();
            }
        }

        public void Heal(float amount)
        {
            currentHealth += amount;
            currentHealth = Mathf.Min(currentHealth , maxHealth);
        }

        public void OnDead()
        {
            isAlive = false;
            OnDeadEvent?.Invoke();
        }

        private void HitStop(ActionData empty)
        {
            StartCoroutine(HitStopRoutine());
        }

        private IEnumerator HitStopRoutine()
        {
            Time.timeScale = 0.24f;
            yield return new WaitForSecondsRealtime(0.15f);
            Time.timeScale = 1;
        }
    }
}

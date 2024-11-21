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

        public static event Action OnDeadEvent;
        public static event Action<ActionData> OnHitEvent;
        public static event Action OnHealEvent;
        [SerializeField] private float camShakePower;
        


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
            
            //before stat system
            currentHealth -= ActionData.damageAmount;
            
            OnHitEvent?.Invoke(actiondata);
            
            if (currentHealth <= 0)
            {
                OnDead();
            }
        }

        public float GetPercent()
        {
            return currentHealth / maxHealth;
        }

        public void Heal(float amount)
        {
            currentHealth += amount;
            currentHealth = Mathf.Min(currentHealth , maxHealth);
            OnHealEvent?.Invoke();
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

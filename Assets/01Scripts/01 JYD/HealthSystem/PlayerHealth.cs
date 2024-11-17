using System;
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

        public event Action OnDeadEvent;
        public event Action OnHitEvent;

        private void Start()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(ActionData actiondata)
        {
            ActionData.knockBackPower = actiondata.knockBackPower;
            ActionData.damageAmount = actiondata.damageAmount;
            ActionData.hitNormal = actiondata.hitNormal;
            ActionData.hitPoint = actiondata.hitPoint;
            
            OnHitEvent?.Invoke();
            
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
    }
}

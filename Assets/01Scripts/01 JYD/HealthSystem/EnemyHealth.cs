using System;
using UnityEngine;

namespace CardGame
{
    public class EnemyHealth : MonoBehaviour ,IDamageable
    {
        public ActionData ActionData;
        
        public float MaxHealth => maxHealth;
        public float CurrentHealth => currentHealth;
        public bool IsAlive => isAlive;
        
        [SerializeField] private float maxHealth;
        [SerializeField] private float currentHealth;
        [SerializeField] private bool isAlive;

        private Agent owner;

        [SerializeField] private ChangeState ChangeState;
        
        public event Action OnDeadEvent;
        public event Action OnHitEvent;
        
        private void Awake()
        {
            owner = GetComponent<Agent>();
        }

        private void Start()
        {
            currentHealth = maxHealth;
            isAlive = true;
        }

        public void TakeDamage(ActionData actionData)
        {
            if(isAlive == false)return;
            
            ActionData.knockBackPower = actionData.knockBackPower;
            ActionData.damageAmount = actionData.damageAmount;
            ActionData.hitNormal = actionData.hitNormal;
            ActionData.hitPoint = actionData.hitPoint;
            
            OnHitEvent?.Invoke();
            
            currentHealth -= ActionData.damageAmount;
            
            if (currentHealth <= 0)
            {
                OnDead();
               
            }

            //owner.GetKnockBack(-owner.transform.forward * ActionData.knockBackPower);
        }

        public void Heal(float amount)
        {
            currentHealth += amount;
            currentHealth = Mathf.Min(currentHealth , maxHealth);
        }

        public void OnDead()
        {
            isAlive = false;
            ChangeState.SendEventMessage(State.Dead);
            OnDeadEvent?.Invoke();
        }

        public float GetHealthPercent()
        {
            return (currentHealth / maxHealth) * 100;
        }
    }
}
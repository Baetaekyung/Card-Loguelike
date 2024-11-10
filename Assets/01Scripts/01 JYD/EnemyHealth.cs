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

        private void Awake()
        {
            owner = GetComponent<Agent>();
        }

        private void Start()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(ActionData actionData)
        {
            ActionData.knockBackPower = actionData.knockBackPower;
            ActionData.damageAmount = actionData.damageAmount;
            ActionData.hitNormal = actionData.hitNormal;
            ActionData.hitPoint = actionData.hitPoint;
            
            if (currentHealth <= 0)
            {
                OnDead();
            }

            owner.GetKnockBack(-owner.transform.forward * ActionData.knockBackPower);
        }

        public void Heal(float amount)
        {
            currentHealth += amount;
            currentHealth = Mathf.Min(currentHealth , maxHealth);
        }

        public void OnDead()
        {
            isAlive = true;
        }

        public float GetHealthPercent()
        {
            return (currentHealth / maxHealth) * 100;
        }
    }
}
using System;
using UnityEngine;

namespace CardGame
{
    public class EnemyHealth : MonoBehaviour ,IDamageable
    {
        public float MaxHealth => maxHealth;
        public float CurrentHealth => currentHealth;
        public bool IsAlive => isAlive;
        
        [SerializeField] private float maxHealth;
        [SerializeField] private float currentHealth;
        [SerializeField] private bool isAlive;

        private void Start()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(float amount)
        {
            currentHealth -= amount;
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
            isAlive = true;
        }

        public float GetHealthPercent()
        {
            return (currentHealth / maxHealth) * 100;
        }
    }
}
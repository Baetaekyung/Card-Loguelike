using UnityEngine;

namespace CardGame
{
    public interface IDamageable
    {
        float MaxHealth { get; }
        float CurrentHealth { get; }
        bool IsAlive { get; }
        
        public void TakeDamage(float amount);
        public void Heal(float amount);
        public void OnDead();
    }
}

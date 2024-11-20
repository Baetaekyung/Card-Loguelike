using UnityEngine;
using ActionData = CardGame.ActionData;

namespace CardGame
{
    public interface IDamageable
    {
        float MaxHealth { get; }
        float CurrentHealth { get; }
        bool IsAlive { get; }
        
        public void TakeDamage(ActionData actiondata);
        public void Heal(float amount);
        public void OnDead();
    }
}

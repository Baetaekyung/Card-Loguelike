using UnityEngine;

namespace CardGame.Weapons
{
    public abstract class BaseWeapon
    {
        [SerializeField] private float delay;

        public virtual void TryAttack()
        {
            bool canAttack = default;
            if(canAttack)
                Attack();
        }
        protected abstract void Attack();
    }
}

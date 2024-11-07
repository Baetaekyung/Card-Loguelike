using UnityEngine;

namespace CardGame.Weapons
{
    public abstract class BaseWeapon
    {
        [SerializeField] private float delay;
        public bool CanAttack
        {
            get
            {
                return default;
            }
        }
        public virtual void TryAttack()
        {
            if(CanAttack)
                Attack();
        }
        protected abstract void Attack();
    }
}

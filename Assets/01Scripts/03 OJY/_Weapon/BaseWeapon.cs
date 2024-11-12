using UnityEngine;

namespace CardGame.Weapons
{
    public abstract class BaseWeapon : MonoBehaviour
    {
        [SerializeField] protected BaseWeaponSO baseWeaponSO;
        private float currentDelayTime;
        protected virtual bool CanAttack => currentDelayTime < Time.time;
        private void Awake()
        {
            transform.position = Vector3.zero;
        }
        public void TryAttack()
        {
            if(CanAttack)
            {

                baseWeaponSO.OnEvent(this);
                currentDelayTime = baseWeaponSO.GetDelay + Time.time;
                Attack();
            }
        }
        protected abstract void Attack();
    }
}

using UnityEngine;

namespace CardGame.Weapons
{
    public abstract class BaseWeapon : MonoBehaviour
    {
        [SerializeField] protected BaseWeaponSO baseWeaponSO;
        [SerializeField] private Transform transformEffect;
        public Transform GetTransformEffect => transformEffect;

        private float currentDelayTime;
        protected virtual bool CanAttack => currentDelayTime < Time.time;

        private void Awake()
        {
            transform.position = Vector3.zero;
        }
        public bool TryAttack()
        {
            if(CanAttack)
            {
                baseWeaponSO.OnEvent(this);
                currentDelayTime = baseWeaponSO.GetDelay + Time.time;
                BaseAttackEvent();
            }
            return CanAttack;
        }
        /// <summary>
        /// for child class
        /// </summary>
        protected virtual void BaseAttackEvent()
        {
            //Debug.LogWarning("dont call me");
        }
        /// <summary>
        /// for child child class
        /// </summary>
        public abstract void OnAnimatoinEventTrigger();
    }
}

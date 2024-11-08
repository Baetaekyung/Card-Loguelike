using UnityEngine;

namespace CardGame.Weapons
{
    public abstract class BaseWeapon : MonoBehaviour
    {
        [SerializeField] protected BaseWeaponSO baseWeaponSO;
        private float currentDelayTime;
        protected virtual bool CanAttack => currentDelayTime < Time.time;
        public void TryAttack()
        {
            if(CanAttack)
            {
                baseWeaponSO.OnEvent();
                currentDelayTime = baseWeaponSO.GetDelay + Time.time;
                Attack();
            }
        }
        private void Update()
        {
            InventoryUI.Instance.GetList[3].text = currentDelayTime + ", " + Time.time;
        }
        protected abstract void Attack();
    }
}

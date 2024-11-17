using UnityEngine;

namespace CardGame.Weapons
{
    public abstract class BaseMeleeSO : BaseWeaponSO
    {
        [Header("OnAttackRegistered")]
        public GameObject prefab;
    }
}

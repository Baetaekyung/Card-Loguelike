using UnityEngine;

namespace CardGame.Weapons
{
    public abstract class BaseWeaponSO : ScriptableObject
    {
        [Header("General")]
        [SerializeField] private float delay;
        public float GetDelay => delay;
        public abstract void OnEvent(BaseWeapon owner);
    }
}

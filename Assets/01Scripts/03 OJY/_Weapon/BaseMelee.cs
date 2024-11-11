using System;
using System.Collections.Generic;
using UnityEngine;

namespace CardGame.Weapons
{
    public abstract class BaseMelee : BaseWeapon
    {
        [SerializeField] protected DamageCaster damageCaster;
        private void Awake()
        {
            damageCaster = GetComponentInChildren<DamageCaster>();
        }
    }
}

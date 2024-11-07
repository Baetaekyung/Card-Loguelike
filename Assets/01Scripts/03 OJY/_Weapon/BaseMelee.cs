using System;
using System.Collections.Generic;
using UnityEngine;

namespace CardGame.Weapons
{
    public abstract class BaseMelee : BaseWeapon
    {
        [SerializeField] protected List<Transform> collisionTransforms;

    }
}


using System;
using UnityEngine;

namespace CardGame
{
    public class IceField : DamageOnCollision
    {
        private void Start()
        {
            removeTime = 2f;
        }
    }
}

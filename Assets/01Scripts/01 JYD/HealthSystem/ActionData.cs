using System;
using UnityEngine;

namespace CardGame
{
    [Serializable]
    public struct ActionData
    {
        public float knockBackPower;
        public float damageAmount;
        
        public Vector3 hitNormal;
        public Vector3 hitPoint;
    }
}
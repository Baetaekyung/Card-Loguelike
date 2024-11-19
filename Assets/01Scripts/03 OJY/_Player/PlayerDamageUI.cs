using DG.Tweening;
using System;
using UnityEngine;

namespace CardGame.Players
{
    public class PlayerDamageUI : MonoBehaviour
    {

        private void Awake()
        {
            PlayerHealth.OnHitEvent += HandleOnHit;
        }

        private void HandleOnHit()
        {

        }
        private void OnDestroy()
        {
            PlayerHealth.OnHitEvent -= HandleOnHit;
        }
    }
}

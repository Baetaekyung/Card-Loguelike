using CardGame.Players;
using System;
using UnityEngine;

namespace CardGame
{
    public class AnimationEventTrigger : MonoBehaviour
    {
        public static event Action OnAttackEventTrigger;
        [SerializeField] private Player player;
        private void AE_AnimationAttack(float damage)
        {
            OnAttackEventTrigger?.Invoke();
        }
        private void AE_OnAnimationEnd()
        {
            player.AnimationEndWeaponTrigger();
        }
        private void AE_PlaySwingSound()
        {
            player.PlaySwingSound();
        }
    }
}

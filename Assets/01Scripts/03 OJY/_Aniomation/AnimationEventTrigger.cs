using System;
using UnityEngine;

namespace CardGame
{
    public class AnimationEventTrigger : MonoBehaviour
    {
        public static event Action OnAttackEventTrigger;
        private void AE_AnimationAttack(float damage)
        {
            OnAttackEventTrigger?.Invoke();
        }
    }
}

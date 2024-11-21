using UnityEngine;

namespace CardGame.FSM.States
{
    public class PS_Death : PS_Base_Movement
    {
        public PS_Death(AnimationParameterSO animParam) : base(animParam)
        {
        }
        protected override void HandleOnMovement(Vector3 input)
        {
        }
        protected override void HandleOnDirectionLook(Vector3 input)
        {
        }
        protected override void HandleOnPlayerAttack()
        {
        }
        protected override void HandleOnRoll()
        {
        }
    }
}

using UnityEngine;

namespace CardGame.FSM.States
{
    public class PS_Running : PS_Base_Movement
    {
        public PS_Running(AnimationParameterSO animParam) : base(animParam)
        {
        }
        protected override void HandleOnMovement(Vector3 input)
        {
            base.HandleOnMovement(input);
            if (input.sqrMagnitude <= 0.25f) StateMachine.ChangeState(PlayerStateEnum.Movement.Idle);
        }
    }
}

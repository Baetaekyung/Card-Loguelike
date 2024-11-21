using UnityEngine;

namespace CardGame.FSM.States
{
    public class PS_Idle : PS_Base_Movement
    {
        public PS_Idle(AnimationParameterSO animParam) : base(animParam)
        {
        }
        protected override void HandleOnMovement(Vector3 input)
        {
            base.HandleOnMovement(input);
            if (input.sqrMagnitude > 0.25) StateMachine.ChangeState(PlayerStateEnum.Movement.Running);
        }
    }
}

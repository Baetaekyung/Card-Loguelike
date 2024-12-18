using System;
using UnityEngine;

namespace CardGame.FSM.States
{
    public class PS_Roll : PS_Base_Movement
    {
        public PS_Roll(AnimationParameterSO animParam) : base(animParam)
        {
        }
        public static event Action OnRoll;
        public override void Enter()
        {
            base.Enter();
            OnRoll?.Invoke();
            Debug.Log("rollin");
            BaseOwner.GetPlayerMovement.DoABarrelRoll(BaseOwner.GetInput.GetCameraRelativeInput, 6, 
                () => 
                {
                    StateMachine.ChangeState(PlayerStateEnum.Movement.Idle);
                });
            //
        }
        protected override void HandleOnDirectionLook(Vector3 input)
        {
        }
        public override void Exit()
        {
            Debug.Log("Roll end");
            base.Exit();
        }
    }
}

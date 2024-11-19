using CardGame.Players;
using System;
using UnityEngine;

namespace CardGame.FSM.States
{
    public abstract class PS_Base_Movement : PS_Base<PlayerStateEnum.Movement, Player>
    {
        protected PS_Base_Movement(AnimationParameterSO animParam) : base(animParam)
        {

        }
        public override void Enter()
        {
            base.Enter();
            SubscribeEvent();
            void SubscribeEvent()
            {
                var inp = BaseOwner.GetInput;
                inp.EventPlayerRoll += HandleOnRoll;
            }
        }
        public override void Update()
        {
            base.Update();
            HandleOnDirectionLook(BaseOwner.GetInput.GetCameraRelativeInputRaw);
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            HandleOnMovement(BaseOwner.GetInput.GetCameraRelativeInputRaw);
        }
        protected virtual void HandleOnDirectionLook(Vector3 input)
        {
            if (BaseOwner.GetInput.IsPressingAnyDirectionKey)
            {
                BaseOwner.GetPlayerRenderer.SetVisualDirection(input);
            }
        }
        protected virtual void HandleOnMovement(Vector3 input)
        {
            BaseOwner.GetPlayerMovement.InputDirection = input;
        }
        protected virtual void HandleOnRoll()
        {
            bool isMoving = BaseOwner.GetInput.KeyNotPressedTime < 0.2f;//0.25~ 0.3 is safe. if legnth of dir > 0
            if (isMoving)
            {
                bool result = BaseOwner.GetPlayerMovement.CanDash;
                if (result) StateMachine.ChangeState(PlayerStateEnum.Movement.Roll);
            }

        }
        public override void Exit()
        {
            void UnSubscribeEvent()
            {
                var inp = BaseOwner.GetInput;
                inp.EventPlayerRoll -= HandleOnRoll;
            }
            UnSubscribeEvent();
            base.Exit();
        }

    }
}

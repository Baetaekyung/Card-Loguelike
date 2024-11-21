using CardGame.Players;
using UnityEngine;

namespace CardGame.FSM.States
{
    public class PS_NormalAttack : PS_Base_Movement
    {
        public PS_NormalAttack(AnimationParameterSO animParam) : base(animParam)
        {
        }

        public override void Enter()
        {
            base.Enter();
            BaseOwner.GetPlayerMovement.AllowInputMoving = false;
            //Debug.Log("attack start");
        }
        public override void Update()
        {
            base.Update();
            if (AnimationEndTrigger)
                StateMachine.ChangeState(PlayerStateEnum.Movement.Idle);
            //Debug.Log("up");
        }
        protected override void HandleOnDirectionLook(Vector3 input)
        {

        }
        public override void Exit()
        {
            base.Exit();
            BaseOwner.GetPlayerMovement.AllowInputMoving = true;
            //Debug.Log("exit");
        }
    }
}

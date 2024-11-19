using UnityEngine;

namespace CardGame.FSM.States
{
    public class PS_NormalAttack : State
    {
        public PS_NormalAttack(AnimationParameterSO animParam) : base(animParam)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("attack start");
        }
        public override void Update()
        {
            base.Update();
            Debug.Log("up");
        }
        public override void Exit()
        {
            base.Exit();
            Debug.Log("exit");
        }
    }
}

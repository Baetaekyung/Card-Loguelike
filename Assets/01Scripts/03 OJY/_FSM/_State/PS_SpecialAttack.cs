using UnityEngine;

namespace CardGame.FSM.States
{
    public class PS_SpecialAttack : State
    {
        public PS_SpecialAttack(AnimationParameterSO animParam) : base(animParam)
        {
        }
        public override void Enter()
        {
            base.Enter();
            Debug.Log("ming");
        }
    }
}

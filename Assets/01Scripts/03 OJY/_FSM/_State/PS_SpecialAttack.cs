using UnityEngine;

namespace CardGame.FSM.States
{
    public class PS_SpecialAttack : PS_Base_Combat
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

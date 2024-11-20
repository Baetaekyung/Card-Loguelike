using CardGame.Players;
using UnityEngine;

namespace CardGame.FSM.States
{
    public abstract class PS_Base_Combat : PS_Base<PlayerStateEnum.Combat, Player>
    {
        protected PS_Base_Combat(AnimationParameterSO animParam) : base(animParam)
        {
        }
    }
}

using CardGame.Players;
using System;

namespace CardGame.FSM.States
{
    public abstract class PS_Base<StateEnum, Owner> : State
        where StateEnum : Enum
        where Owner : Entity
    {
        public static FiniteStateMachine<StateEnum, Owner> StateMachine { get; set; }
        public static Owner BaseOwner { get; set; }
        public PS_Base(AnimationParameterSO animParam) : base(animParam)
        {
        }
    }
}

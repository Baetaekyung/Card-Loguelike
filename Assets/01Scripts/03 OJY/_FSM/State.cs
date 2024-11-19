using System;
using UnityEngine;
namespace CardGame.FSM
{
    public abstract class State
    {
        public Action Current { get; private set; }
        public AnimationParameterSO GetAnimationParam { get; }

        public State(AnimationParameterSO animParam)
        {
            Current = Enter;
            GetAnimationParam = animParam;
        }
        public virtual void Enter()
        {
            Current = Update;
        }
        public virtual void Update()
        {
        }
        public virtual void FixedUpdate()
        {
        }
        public virtual void Exit()
        {
        }
    }
}
using System;
using UnityEngine;
namespace CardGame.FSM
{
    public abstract class State
    {
        public Action Current { get; private set; }

        public State()
        {
            Current = Enter;
        }
        public virtual void Enter()
        {
            //logic
            Current = Update;
        }
        public virtual void Update()
        {
            //logic
        }

        public virtual void Exit()
        {
            //logic
        }
    }
}
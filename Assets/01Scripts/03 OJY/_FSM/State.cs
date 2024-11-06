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
            Current = Update;
        }
        public virtual void Update()
        {
        }
        public virtual void Exit()
        {
        }
    }
}
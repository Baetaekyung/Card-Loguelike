using System;
using System.Collections.Generic;
namespace CardGame.FSM
{
    public class FiniteStateMachine<StateEnum> where StateEnum : Enum
    {
        public State CurrentState { get; protected set; }
        public Dictionary<StateEnum, State> StateDictionary { get; } = new();
    
        public void AddState(StateEnum type, State instance)
        {
            StateDictionary.Add(type, instance);
        }
        public void Initialize(StateEnum state)
        {
            CurrentState = StateDictionary[state];
        }
        public void ChangeState(StateEnum type)
        {
            CurrentState.Exit();
            CurrentState = StateDictionary[type];
            CurrentState.Enter();
        }
        public void UpdateState()
        {
            CurrentState.Current?.Invoke();
        }
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CardGame.FSM
{
    //ihatefsm
    public class FiniteStateMachine<StateEnum, Owner> where StateEnum : Enum where Owner : MonoBehaviour
    {
        public State CurrentState { get; protected set; }
        private Dictionary<StateEnum, State> StateDictionary { get; } = new();
        private readonly Owner owner;

        public FiniteStateMachine(Owner owner)
        {
            this.owner = owner;
        }
        public Owner GetOwner => owner;
        /// <summary>
        /// unfinished
        /// </summary>
        public void Initialize()
        {
            Array enumValues = Enum.GetValues(typeof(StateEnum));
            for (int i = 0; i < enumValues.Length; i++)
            {
                //Debug.Log(StateEnum(i));
            }
        }
        public void AddState(StateEnum type, State instance)
        {
            if (StateDictionary.ContainsKey(type))
            {
                Debug.LogError("[Error] Adding already existing state to dictionary");
                return;
            }
            StateDictionary.Add(type, instance);
        }
        public void SetCurrentState(StateEnum state)
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
using System;
using Unity.Behavior.GraphFramework;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/ChangeState")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "ChangeState", message: "AgentObj Change [State]", category: "Events", id: "2a9e0f248dd709d7978499ebd1f015c0")]
public partial class ChangeState : EventChannelBase
{
    public delegate void ChangeStateEventHandler(State State);
    public event ChangeStateEventHandler Event; 

    public void SendEventMessage(State State)
    {
        Event?.Invoke(State);
    }

    public override void SendEventMessage(BlackboardVariable[] messageData)
    {
        BlackboardVariable<State> StateBlackboardVariable = messageData[0] as BlackboardVariable<State>;
        var State = StateBlackboardVariable != null ? StateBlackboardVariable.Value : default(State);

        Event?.Invoke(State);
    }

    public override Delegate CreateEventHandler(BlackboardVariable[] vars, System.Action callback)
    {
        ChangeStateEventHandler del = (State) =>
        {
            BlackboardVariable<State> var0 = vars[0] as BlackboardVariable<State>;
            if(var0 != null)
                var0.Value = State;

            callback();
        };
        return del;
    }

    public override void RegisterListener(Delegate del)
    {
        Event += del as ChangeStateEventHandler;
    }

    public override void UnregisterListener(Delegate del)
    {
        Event -= del as ChangeStateEventHandler;
    }
}


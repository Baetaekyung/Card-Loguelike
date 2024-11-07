using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "WaitForAnimationEnd", story: "Wait For [AnimationEnd] in [AgentObj]", category: "Conditions", id: "3d28ccf60726aa55d5e44073eb1ee254")]
public partial class WaitForAnimationEndCondition : Condition
{
    [SerializeReference] public BlackboardVariable<bool> AnimationEnd;
    [SerializeReference] public BlackboardVariable<Agent> Agent;
    
    
    
    public override bool IsTrue()
    {
        if (Agent.Value.AnimationEnd)
        {
            return true;
        }
        
        
        return false;
    }

    public override void OnStart()
    {
        
    }

    public override void OnEnd()
    {
        Agent.Value.StopAnimationEnd();
    }
}

using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "agent to target in radius", story: "[Agent] to [Target] in [Radius]", category: "Conditions", id: "ab294a8ed81f39d04fdafeb4d41c801f")]
public partial class AgentToTargetInRadiusCondition : Condition
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<float> Radius;

    public override bool IsTrue()
    {
        if (Agent.Value == null|| Target.Value == null)
        {
            return false;
        }
                
        
        return CheckDistance();
    }

    private bool CheckDistance()
    {
        float distance = Vector3.Distance(Agent.Value.transform.position, Target.Value.position);
        return distance <= Radius.Value;
    }
    
    
}

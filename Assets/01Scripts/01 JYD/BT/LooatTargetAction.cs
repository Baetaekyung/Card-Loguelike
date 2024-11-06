using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "LookTarget", story: "[Agent] Look [Target] [Speed]", category: "Action", id: "d71636ab488f2eabdd67220f700c7876")]
public partial class LooatTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<float> Speed;
    
    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        Vector3 lookDir = Target.Value.position - Agent.Value.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(lookDir);
               
        Agent.Value.transform.rotation = Quaternion.Slerp(
            Agent.Value.transform.rotation,
            lookRotation,
            Speed.Value * Time.deltaTime
        );

        Debug.Log(Quaternion.Angle(Agent.Value.transform.rotation, lookRotation));
        
        if (Quaternion.Angle(Agent.Value.transform.rotation, lookRotation) < 1.0f)
        {
            return Status.Success;
        }
                
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}


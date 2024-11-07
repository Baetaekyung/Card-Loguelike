using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IsTargetMove", story: "[Agent] Check [Target] Move In [Radius]", category: "Conditions", id: "fdf7b232160399e64b7203c09402cdab")]
public partial class IsTargetMoveCondition : Condition
{
    [SerializeReference] public BlackboardVariable<Transform> Agent;
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<float> Radius;
    private NavMeshAgent NavMeshAgent;
    
    public override bool IsTrue()
    {
        float distance = Vector3.Distance(Target.Value.transform.position ,NavMeshAgent.destination);
        
        Debug.Log($"distance is {distance} , Radius is {Radius}");
        
        if (distance > Radius.Value)
        {
            return true;
        }
        
        return false;
    }

    public override void OnStart()
    {
        if(NavMeshAgent == null)
            NavMeshAgent = Agent.Value.GetComponent<NavMeshAgent>();
    }

    public override void OnEnd()
    {
    }
}

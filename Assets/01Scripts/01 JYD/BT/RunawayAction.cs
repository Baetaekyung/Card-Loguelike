using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Runaway", story: "[Agent] Runaway From [Target]", category: "Action", id: "a07a124133e10946d4356e61b94d8b98")]
public partial class RunawayAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<Transform> Target;
    
    [SerializeReference] public BlackboardVariable<float> runAwayDistance;

    private NavMeshAgent NavMeshAgent;
    
    protected override Status OnStart()
    {
        if (NavMeshAgent == null)
            NavMeshAgent = Agent.Value.GetComponent<NavMeshAgent>();
        
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        Vector3 normalDir = (Target.Value.transform.position - Agent.Value.transform.position).normalized;

        if (Vector3.Distance(normalDir, Agent.Value.transform.position) <= 0.2f)
            return Status.Success;
        
        
        NavMeshAgent.SetDestination(Agent.Value.transform.position - (normalDir * runAwayDistance.Value));
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}


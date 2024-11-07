using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetStop", story: "Set [AgentObj] Stop [active]", category: "Action", id: "ea4680d42f6fd995ce8b7ddafddc1c17")]
public partial class SetStopAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<bool> Active;

    private NavMeshAgent _navMeshAgent;
    
    protected override Status OnStart()
    {
        if (_navMeshAgent == null)
        {
            _navMeshAgent = Agent.Value.GetComponent<NavMeshAgent>();
        }
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        _navMeshAgent.isStopped = Active.Value;
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}


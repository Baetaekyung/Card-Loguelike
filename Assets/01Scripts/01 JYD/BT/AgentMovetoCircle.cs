using System;
using Unity.Behavior;
using Unity.Cinemachine;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Agent Moveto Circle", story: "[Agent] MoveTo [Target] In Circle and stopOffset is [AttackRadius]", category: "Action", id: "f3abb3e9c41ecafe5b0ff5fce0c3ba14")]
public partial class AgentMovetoCircleAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<float> AttackRadius;
    
    private NavMeshAgent _navMeshAgent;
    private Agent _agent;
    
    AIManager aiManager; 
        
    protected override Status OnStart()
    {
        #region Initialize
       
        if (aiManager == null)
        {
            aiManager = AIManager.Instance;
        }
        
        if (_navMeshAgent == null)
        {
            _navMeshAgent = Agent.Value.GetComponent<NavMeshAgent>();
            
        }

        if (_agent == null)
        {
            _agent = Agent.Value.GetComponent<Agent>();
        }

        #endregion
        
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        Vector3 origin = Target.Value.transform.position;
        
        //Debug.Log(origin);
        
        float distanceToTarget = Vector3.Distance(_navMeshAgent.destination, origin);
    
        if (distanceToTarget <= AttackRadius)
        {
            return Status.Success;
        }

        int agentCount = aiManager.GetAgentCount();
        int agentIndex = aiManager.GetAgentIndex(_agent);

        Vector3 newPosition = new Vector3
        (
            origin.x + AttackRadius.Value / 2 * Mathf.Cos(2 * Mathf.PI * agentIndex / agentCount),
            origin.y,
            origin.z + AttackRadius.Value / 2 * Mathf.Sin(2 * Mathf.PI * agentIndex / agentCount)
        );

        _navMeshAgent.SetDestination(newPosition);
        return Status.Running;
    }

    protected override void OnEnd()
    {
        // 필요한 경우 종료 처리
    }
}


using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "AgentObj Can Attack ", story: "[AgentObj] Close To [Target] Destination [AttackRadius] [Inverter]", category: "Conditions", id: "9c6499257f47a8a809ec3b9e8d87d321")]
public partial class AgentCanAttackCondition : Condition
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<float> AttackRadius;
    
    private NavMeshAgent navMeshAgent;

    private float stopOffset = 0.2f;
    
    public override bool IsTrue()
    {
        if (navMeshAgent == null || Agent.Value == null) return false;

        bool checkCloseToDestination = navMeshAgent.remainingDistance < stopOffset;
        bool checkCloseToTarget = Mathf.Round(Vector3.Distance(Agent.Value.transform.position , Target.Value.transform.position)) <= AttackRadius.Value;

        
        
        return (checkCloseToDestination && checkCloseToTarget);
    }

    public override void OnStart()
    {
        if (navMeshAgent == null)
        {
            navMeshAgent = Agent.Value.GetComponent<NavMeshAgent>();
        }
    }

    public override void OnEnd()
    {
    }
}

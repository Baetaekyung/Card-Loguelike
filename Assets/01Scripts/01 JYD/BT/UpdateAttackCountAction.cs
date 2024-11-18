using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "UpdateAttackCount", story: "Update [AttackCount]", category: "Action", id: "c107c10efbb7d7136fecf034396a7885")]
public partial class UpdateAttackCountAction : Action
{
    [SerializeReference] public BlackboardVariable<int> AttackCount;
    [SerializeReference] public BlackboardVariable<int> MaxCount;
    [SerializeReference] public BlackboardVariable<float> AttackDuration;
    
    private float lastAttackTime;
    
    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        ++AttackCount.Value;
        if (AttackCount.Value > MaxCount.Value || Time.time > lastAttackTime + AttackDuration.Value)
            AttackCount.Value = 0;
        
        lastAttackTime = Time.time;
        
        return Status.Success;
    }

    protected override void OnEnd()
    {
        lastAttackTime = Time.time;
    }
}


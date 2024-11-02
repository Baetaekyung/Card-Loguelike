using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "CheckDetected", story: "[Agent] Detected [Target] In [Radius]", category: "Conditions", id: "5fecee9566241d40ced82c4082d57414")]
public partial class CheckDetectedCondition : Condition
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<float> Radius;
    
    public override bool IsTrue()
    {
        
        
        
        return true;
    }

    public override void OnStart()
    {
        Debug.Log("여기서 충돌 체크를 검사하면 되나??");
    }

    public override void OnEnd()
    {
    }
}

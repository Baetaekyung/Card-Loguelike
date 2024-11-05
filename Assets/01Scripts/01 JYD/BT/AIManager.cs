using System;
using UnityEngine;
using UnityEngine.UIElements;

public class AIManager : MonoSingleton<AIManager>
{
    [SerializeField] private Agent[] agents;
    [SerializeField] private float radius;
    public float Radius => radius;
    public int GetAgentCount() => agents.Length;
    public int GetAgentIndex(Agent agent) => Array.IndexOf(agents, agent);


}

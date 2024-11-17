using System;
using System.Collections.Generic;
using UnityEngine;




public class AIManager : MonoSingleton<AIManager>
{
    [SerializeField] private List<Agent> agents; 
    public int GetAgentCount() => agents.Count;
    public int GetAgentIndex(Agent agent) => Array.IndexOf(agents.ToArray(), agent);

    protected override void Awake()
    {
        base.Awake();

        
        
    }

    private void Start()
    {
        Agent[] currentAgents = FindObjectsByType<Agent>(FindObjectsInactive.Include, FindObjectsSortMode.None);
                    
        for (int i = 0; i < currentAgents.Length; i++)
        {
            agents.Add(currentAgents[i]);
        }
    }
}

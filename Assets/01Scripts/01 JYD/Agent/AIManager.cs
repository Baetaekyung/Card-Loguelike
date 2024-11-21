using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;




public class AIManager : MonoSingleton<AIManager>
{
    [SerializeField] private List<Agent> agents = new(); 
    public int GetAgentCount() => agents.Count;
    public int GetAgentIndex(Agent agent) => Array.IndexOf(agents.ToArray(), agent);

    private void Start()
    {
        Agent[] currentAgents = FindObjectsByType<Agent>(FindObjectsInactive.Include, FindObjectsSortMode.None);
                    
        for (int i = 0; i < currentAgents.Length; i++)
        {
            agents.Add(currentAgents[i]);
        }
        
        
    }

}

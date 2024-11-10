using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;




public class AIManager : MonoSingleton<AIManager>
{
    [SerializeField] private List<Agent> agents; 
    public int GetAgentCount() => agents.Count;
    public int GetAgentIndex(Agent agent) => Array.IndexOf(agents.ToArray(), agent);

    protected override void Awake()
    {
        base.Awake();

        int index = transform.childCount;
        for (int i = 0; i < index; i++)
        {
            agents.Add(transform.GetChild(i).GetComponent<Agent>());
        }
        
    }
}
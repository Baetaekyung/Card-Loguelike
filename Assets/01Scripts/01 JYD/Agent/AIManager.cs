using System;
using System.Collections.Generic;
using UnityEngine;




public class AIManager : MonoSingleton<AIManager>
{
    [SerializeField] private List<Agent> agents; 
    public int GetAgentCount() => agents.Count;
    public int GetAgentIndex(Agent agent) => Array.IndexOf(agents.ToArray(), agent);

    private void Start()
    {
        int index = transform.childCount;
        for (int i = 0; i < index; i++)
        {
            agents.Add(transform.GetChild(i).GetComponent<Agent>());
        }
    }
}

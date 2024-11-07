using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace CardGame
{
    public class ObjectStat : MonoBehaviour
    {
        private Dictionary<string, StatSO> _statDictionary = new Dictionary<string, StatSO>();
        [SerializeField] private StatListSO _statList;
        private void Start()
        {
            foreach (StatSO stat in _statList.statList)
            {
                _statDictionary.Add(stat.statName, stat);
            }
        }

        public StatSO GetStat(string statName)
        {
            StatSO stat = _statDictionary[statName];
            Debug.Assert(stat != null);

            return stat;
        }

        public void SetModifier(string statName, float value)
        {
            StatSO stat = GetStat(statName);
            stat.AddModifier(statName, value);
        }
        
        public void RemoveModifier(string statName)
        {
            StatSO stat = GetStat(statName);
            stat.RemoveModifier(statName);
        }
        
    }
}

using System.Collections.Generic;
using UnityEngine;

namespace CardGame
{
    [CreateAssetMenu(fileName = "StatListSO", menuName = "SO/Stat/StatListSO")]
    public class StatListSO : ScriptableObject
    {
        public List<StatSO> statList;
    }
}

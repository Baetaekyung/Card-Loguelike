using UnityEngine;

namespace CardGame
{
    [CreateAssetMenu(menuName = "OJYSO/EnemySpawnSO", order = int.MinValue)]
    public class EnemySpawnSO : ScriptableObject
    {
        [SerializeField] private GameObject prefab;
        public GameObject GetPrefab => prefab;
    }
}

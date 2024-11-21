using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace CardGame
{
    [Serializable]
    public struct EnemySpawnStrcutList
    {
        [Serializable]
        public struct EnemySpawnStruct
        {
            public EnemySpawnSO enemySpawnSO;
            public List<Transform> spawnPos;
        }
        public List<EnemySpawnStruct> enemyInfos;
    }

    public class EnemySpawnManager : MonoSingleton<EnemySpawnManager>
    {
        [SerializeField] private List<EnemySpawnStrcutList> listOfEnemySpawn;
        [SerializeField] private List<GameObject> enemys;
        
        public void SpawnEnemy(int currentWave)
        {
            int index = currentWave - 1;
            index = Mathf.Max(index , 0);
            if (currentWave % 5 == 0)
            {
                BossSpawn();
                SpawnEnemyByIndex(index);
                return;
            }
            else
            {
                EnemySpawn();
                SpawnEnemyByIndex(index);
            }
        }

        private void BossSpawn()
        {
            //print("boss Enemy");
        }

        private void EnemySpawn()
        {
            //print("normal Enemy");
        }

        private void SpawnEnemyByIndex(int index)
        {
            var ls = listOfEnemySpawn[index];
            var ls2 = ls.enemyInfos;
            foreach (var item in ls2)
            {
                foreach (var item2 in item.spawnPos)
                {
                    GameObject newEnemy = Instantiate(item.enemySpawnSO.GetPrefab, item2.position, item2.rotation, AIManager.Instance.transform);
                    enemys.Add(newEnemy);
                }
            }
        }

        public void RemoveEnemy(GameObject obj)
        {
            enemys.Remove(obj);
            if (enemys.Count == 0)
            {
                WaveManager.Instance.ChangeWave(SceneEnum.SceneDeckSelect);
            }
            
        }
    }
}

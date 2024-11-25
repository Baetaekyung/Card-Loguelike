using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

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
            int max = listOfEnemySpawn.Count - 1;
            index = Mathf.Clamp(index, 0, max);
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
                var waveText = UIManager.Instance.waveText;
                var waveTextTransform = waveText.rectTransform;
                waveText.SetText($"-Wave {WaveManager.CurrentWave} End-");
                waveText.gameObject.SetActive(true);
                waveText.color = new Color(waveText.color.r, waveText.color.g, waveText.color.b, 0); 
                waveTextTransform.anchoredPosition = new Vector2(waveTextTransform.anchoredPosition.x, waveTextTransform.anchoredPosition.y - 100); // 아래로 이동

                waveText.DOFade(1, 0.4f)
                    .OnComplete(() =>
                    {
                        waveTextTransform.DOMoveY(waveTextTransform.anchoredPosition.y + 100, 0.6f) 
                            .SetEase(Ease.InOutQuad)
                            .OnComplete(() =>
                            {
                                waveText.DOFade(0, 0.4f);
                                waveTextTransform.DOMoveY(waveTextTransform.anchoredPosition.y - 100, 0.6f)
                                    .SetEase(Ease.InOutQuad)
                                    .OnComplete(() =>
                                    {
                                        SceneManagerEx.Instance.ChangeScene(SceneEnum.SceneDeckSelect);
                                    });
                            });
                    });
            }
        }
    }
}

using System;
using System.Collections;
using UnityEngine;

namespace CardGame
{
    public class EnemyVFX : MonoBehaviour
    {
        [SerializeField] private EnemyHealth EnemyHealth;
        [SerializeField] private SkinnedMeshRenderer[] enemyMesh;
        [SerializeField] private Material flashMat;
        [SerializeField] private float flashTime;
        
        private Material[] originMat = new Material[10];
        
        private void Start()
        {
            EnemyHealth.OnHitEvent += StartHitFlash;

            for (int i = 0; i < enemyMesh.Length; i++)
            {
                originMat[i] = enemyMesh[i].material;
            }
        }

        private void OnDestroy()
        {
            EnemyHealth.OnHitEvent -= StartHitFlash;
        }

        private void StartHitFlash()
        {
            StopAllCoroutines();
            StartCoroutine(HitFlash());
        }
        

        private IEnumerator HitFlash()
        {

            for (int i = 0; i < enemyMesh.Length; i++)
            {
                enemyMesh[i].material = flashMat;
            }
            
            yield return new WaitForSeconds(flashTime);
            
            for (int i = 0; i < enemyMesh.Length; i++)
            {
                enemyMesh[i].material = originMat[i];
            }
        }
    }
}

using System;
using UnityEngine;

namespace CardGame
{
    public class PlayerVFX : MonoBehaviour
    {
        private PlayerHealth playerHealth;

        [SerializeField] private ParticleSystem hitImpact;
        
        private void Start()
        {
            playerHealth = GetComponent<PlayerHealth>();

            PlayerHealth.OnHitEvent += PlayHitImpact;
        }

        private void PlayHitImpact(ActionData actionData)
        {
            hitImpact.transform.position = actionData.hitPoint;
            hitImpact.transform.rotation = Quaternion.LookRotation(actionData.hitNormal);
            
            hitImpact.Simulate(0);
            hitImpact.Play();
        }

        private void OnDestroy()
        {
            PlayerHealth.OnHitEvent -= PlayHitImpact;
        }
    }
}

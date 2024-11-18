using System;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;


namespace CardGame
{
    [RequireComponent(typeof(CinemachineImpulseSource))]
    public class CameraShaker : MonoBehaviour
    {
        [SerializeField] private CinemachineImpulseSource impulseSource;
        [SerializeField] private PlayerHealth playerHealth;
        private void Start()
        {
            impulseSource = GetComponent<CinemachineImpulseSource>();
            
            playerHealth.OnHitEvent += CameraShake;
        }

        private void OnDestroy()
        {
            playerHealth.OnHitEvent -= CameraShake;
        }

        private void CameraShake(ActionData impulse)
        {
            impulseSource.GenerateImpulseWithForce(impulse.damageAmount);
        }
    }
}

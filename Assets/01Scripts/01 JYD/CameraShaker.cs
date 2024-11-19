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
            
            PlayerHealth.OnHitEvent += CameraShake;
        }

        private void OnDestroy()
        {
            PlayerHealth.OnHitEvent -= CameraShake;
        }

        private void CameraShake(ActionData impulse)
        {
            impulseSource.GenerateImpulseWithForce(impulse.damageAmount);
        }
    }
}

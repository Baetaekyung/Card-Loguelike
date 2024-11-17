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
        
        private void Start()
        {
            impulseSource = GetComponent<CinemachineImpulseSource>();
        }
        
        public void CameraShake(float impulse)
        {
            impulseSource.GenerateImpulseWithForce(impulse);
        }
    }
}

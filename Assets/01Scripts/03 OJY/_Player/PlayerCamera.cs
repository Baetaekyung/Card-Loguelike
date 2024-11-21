using CardGame.GameEvent;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

namespace CardGame.Players
{
    public class PlayerCamera : MonoBehaviour, IPlayerComponent
    {
        private Camera playerCamera;
        [SerializeField] private Transform cinemachineCamera;
        [SerializeField] private CinemachineImpulseSource impulseSource;
        #region Getter/Setter
        public Transform GetCameraTransform => cinemachineCamera.transform;
        public Quaternion GetCameraRotationOnlyY
        {
            get
            {
                Vector3 resultVector = cinemachineCamera.transform.rotation.eulerAngles;
                resultVector.x = 0;
                resultVector.z = 0;
                Quaternion result = Quaternion.Euler(resultVector);
                return result;
            }
        }

        #endregion
        public void Init(Player _player)
        {
            playerCamera = GetComponentInChildren<Camera>();
            impulseSource = GetComponentInChildren<CinemachineImpulseSource>();
            EventManager.AddListener<EventCameraShake>(CameraShake);
        }
        private void CameraShake(EventCameraShake eventCameraShake)
        {
            float value = eventCameraShake.impulse;
            impulseSource.GenerateImpulseWithForce(value);
        }
        public void RotateCamera(float y)
        {
            cinemachineCamera.transform.RotateAround(cinemachineCamera.position, Vector3.up, y);
        }
        public void Dispose(Player _player)
        {
            EventManager.RemoveListener<EventCameraShake>(CameraShake);
        }
    }
}

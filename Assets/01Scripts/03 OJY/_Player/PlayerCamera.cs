using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

namespace CardGame.Players
{
    public class PlayerCamera : MonoBehaviour, IPlayerComponent
    {
        private Camera playerCamera;
        [SerializeField] private CinemachineImpulseSource impulseSource;
        #region Getter/Setter
        public Transform GetCameraTransform => playerCamera.transform;
        public Quaternion GetCameraRotationOnlyY
        {
            get
            {
                Vector3 resultVector = playerCamera.transform.rotation.eulerAngles;
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
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.X))
                CameraShake(5);
        }
        public void CameraShake(float impulse)
        {
            impulseSource.GenerateImpulseWithForce(impulse);
        }
        public void Dispose(Player _player)
        {
        }
    }
}

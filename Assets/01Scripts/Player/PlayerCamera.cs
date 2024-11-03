using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

namespace CardGame.Players
{
    public class PlayerCamera : MonoBehaviour, IPlayerComponent
    {
        private CinemachineCamera playerCamera;
        #region Getter/Setter
        public Transform GetCameraTransform => playerCamera.transform;
        public Quaternion GetCameraRotationOnlyY
        {
            get
            {
                static Quaternion ForgetY(Quaternion quat)
                {
                    Vector3 resultVector = quat.eulerAngles;
                    resultVector.x = 0;
                    resultVector.z = 0;
                    Quaternion result = Quaternion.Euler(resultVector);
                    return result;
                }
                return ForgetY(playerCamera.transform.rotation);
            }
        }

        #endregion
        public void Init(Player _player)
        {
            playerCamera = GetComponentInChildren<CinemachineCamera>();
        }
        public void Dispose(Player _player)
        {
        }
    }
}

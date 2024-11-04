using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardGame.Players
{
    public class PlayerInput : MonoBehaviour, IPlayerComponent
    {
        [Header("Debug")]
        [SerializeField] private float d;
        [SerializeField] private float maxMag;

        #region Getter/Setter
        public Vector3 InputMovementDirection { get; private set; }
        public Vector3 GetCameraRelativeInput => GetCameraRotation * InputMovementDirection.normalized;
        private Quaternion GetCameraRotation => playerCamera.GetCameraRotationOnlyY;
        #endregion

        public Vector3 GetCameraRelativeDirection(Vector3 vector) => GetCameraRotation * vector;

        public event Action EventPlayerRoll;
        [Header("Private Members")]
        private PlayerCamera playerCamera;
        public void Init(Player _player)
        {
            playerCamera = _player.GetPlayerComponent<PlayerCamera>();
        }
        public void Dispose(Player _player)
        {

        }
        private void Update()
        {
            void LegacyInput()
            {
                InputMovementDirection = new Vector3(Input.GetAxisRaw("Horizontal"),0, Input.GetAxisRaw("Vertical"));
                if (Input.GetKeyUp(KeyCode.Space)) EventPlayerRoll?.Invoke();
            }
            LegacyInput();
        }

        
    }
}

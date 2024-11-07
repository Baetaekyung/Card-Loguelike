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
        //util getter
        public Vector3 GetCameraRelativeInput => GetCameraRotation * InputMovementDirection.normalized;
        public bool IsPressingAnyDirectionKey => InputMovementDirection.sqrMagnitude > 0;
        #endregion

        private Quaternion GetCameraRotation => playerCamera.GetCameraRotationOnlyY;
        public Vector3 GetCameraRelativeDirection(Vector3 vector) => GetCameraRotation * vector;

        public event Action EventPlayerRoll;
        public event Action EventPlayerAttack;

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
                if (Input.GetKeyDown(KeyCode.Mouse0)) EventPlayerAttack?.Invoke();
            }
            LegacyInput();
        }

        
    }
}

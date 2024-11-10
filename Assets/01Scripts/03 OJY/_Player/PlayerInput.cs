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
        public Vector3 InputMovementRaw { get; private set; }
        public Vector3 InputMovement { get; private set; }
        //util getter
        public Vector3 GetCameraRelativeInputRaw => GetCameraRotation * InputMovementRaw.normalized;
        public Vector3 GetCameraRelativeInput    => GetCameraRotation * InputMovement.normalized;
        public float KeyNotPressedTime { get; private set; }
        public bool IsPressingAnyDirectionKey => InputMovementRaw.sqrMagnitude > 0;
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
                InputMovementRaw = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
                InputMovement    = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                UI_DEBUG.Instance.GetList[0].text = InputMovement.ToString();
                UI_DEBUG.Instance.GetList[1].text = KeyNotPressedTime.ToString("F2");
                if (Input.GetKeyUp(KeyCode.Space)) EventPlayerRoll?.Invoke();
                if (Input.GetKeyDown(KeyCode.Mouse0)) EventPlayerAttack?.Invoke();
            }
            if (!IsPressingAnyDirectionKey) KeyNotPressedTime += Time.deltaTime;
            else KeyNotPressedTime = 0;
            LegacyInput();
        }

        
    }
}

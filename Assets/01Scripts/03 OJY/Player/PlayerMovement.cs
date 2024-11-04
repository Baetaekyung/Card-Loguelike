using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardGame.Players
{
    public class PlayerMovement : MonoBehaviour, IPlayerComponent
    {
        [Header("Movement Settings")]
        [SerializeField] private float speed;
        [SerializeField] private float onGroundYVal;
        [SerializeField] private float gravitiy = -9.81f;
        [SerializeField] private float gravitiyMultiplier = 1;

        [Header("Roll Settings")]
        [SerializeField] private AnimationCurve rollCurve;
        private Vector3 velocitiy;
        //[Header("GroundDetection")]
        //private readonly Vector3 v = new(0, -0.5f, 0);
        //private const float r = 0.5f;

        [Header("Private Members")]
        private CharacterController characterController;

        #region Getter/Setter
        public Vector3 InputDirection { get; set; }
        public Vector3 GetVelocitiy
        {
            get => velocitiy;
            set => velocitiy = value;
        }
        public Vector3 RollForce { get; private set; }
        public bool IsGround => characterController.isGrounded;
        #endregion
        
        public void Init(Player _player)
        {
            characterController = this.GetOrAddComponent<CharacterController>();
        }
        public void Dispose(Player _player)
        {
        }
        private void Update()
        {
            Vector3 currentVelocity = velocitiy;
            if (IsGround && currentVelocity.y < 0) currentVelocity.y = onGroundYVal;
            else currentVelocity.y += Time.deltaTime * gravitiy * gravitiyMultiplier;

            velocitiy = currentVelocity;
            Vector3 zero = Vector3.zero;
            zero.y = currentVelocity.y;

            velocitiy = Vector3.MoveTowards(currentVelocity, zero, Time.deltaTime * 10);

            InventoryUI.Instance.GetList[0].text = velocitiy.ToString();
            InventoryUI.Instance.GetList[1].text = currentVelocity.y.ToString();
        }
        private void FixedUpdate()
        {
            ApplyMovement();
            //CheckIsGround();
            //void CheckIsGround()
            //{
            //    //bool result = Physics.CheckSphere(transform.position - v, r, ~Game.Layers.lm_Player.value);
            //    //IsGround = result;
            //    //InventoryUI.Instance.GetList[0].text = result.ToString();
            //}
        }
        private void ApplyMovement()
        {
            Vector3 additionalForce = RollForce;
            Vector3 result = InputDirection + velocitiy + additionalForce;
            result *= Time.fixedDeltaTime * speed;
            characterController.Move(result);
        }
        public void AddForce(Vector3 vector, float force = 1)
        {
            velocitiy += vector * force;
        }
        public void DoABarrelRoll(Vector3 vector, int force)
        {
            StopAllCoroutines();
            StartCoroutine(CO_DoABarrelRoll());
            IEnumerator CO_DoABarrelRoll()
            {
                RollForce = vector * force; 
                Vector3 startVelocitiy = RollForce;
                float timer = 0;
float endTime = 1;
                while (timer <= endTime)
                {
                    float curveValue = timer / endTime;
                    float val = rollCurve.Evaluate(curveValue);
                    RollForce = Vector3.Lerp(startVelocitiy, Vector3.zero, val);
                    timer += Time.deltaTime;
                    yield return null;
                }
            }
        }
    }
}

using CardGame.GameEvent;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardGame.Players
{
    public class PlayerMovement : MonoBehaviour, IPlayerComponent, IPlayerComponentStartInit
    {
        [Header("Movement Settings")]
        [SerializeField] private float speed;
        [SerializeField] private float onGroundYVal;
        [SerializeField] private float gravitiy = -9.81f;
        [SerializeField] private float gravitiyMultiplier = 1;
        private Vector3 velocitiy;

        [Header("Roll Settings")]
        [SerializeField] private AnimationCurve rollCurve; // curve length should be 1.
        [SerializeField] private float db_stmod;
        private const float rollcost = 1f;
        private const float initialRollStamina = 3f;
        public float GetMaxStamina => initialRollStamina + db_stmod;
        private float rollStamina = initialRollStamina;
        public float GetCurrentStamina => rollStamina;

        //[Header("GroundDetection")]
        //private readonly Vector3 v = new(0, -0.5f, 0);
        //private const float r = 0.5f;

        [Header("Animation Parameter")]
        [SerializeField] private AnimationParameterSO movementXParam;
        [SerializeField] private AnimationParameterSO movementYParam;
        [SerializeField] private AnimationParameterSO fallParam;

        private CharacterController characterController;
        private PlayerAnimator playerAnimator;
        #region Getter/Setter
        public Vector3 InputDirection { get; set; }
        public Vector3 GetVelocitiy => characterController.velocity;
        public Vector3 RollForce { get; private set; }
        public bool AllowInputMoving { get; set; } = true;
        public bool IsMoving => characterController.velocity.sqrMagnitude > 0.25f;
        public bool IsGround => characterController.isGrounded;
        public bool CanDash => rollStamina > 1;
        #endregion

        public void Init(Player _player)
        {
            characterController = this.GetOrAddComponent<CharacterController>();
        }
        public void StartInit(Player _player)
        {
            playerAnimator = _player.GetPlayerComponent<PlayerAnimator>();
        }
        public void Dispose(Player _player)
        {
        }
        private void Update()
        {
            void ApplyMovement()
            {
                ref float y = ref velocitiy.y;
                if (IsGround && y < 0) y = onGroundYVal;
                else y += Time.deltaTime * gravitiy * gravitiyMultiplier;

                Vector3 zero = Vector3.zero;
                zero.y = y;

                velocitiy = Vector3.MoveTowards(velocitiy, zero, Time.deltaTime * 10);
            }
            void ApplyAnimator()
            {
                Vector3 currentVelocitiyNormalized = GetVelocitiy.normalized;
                playerAnimator.SetParam(movementXParam, currentVelocitiyNormalized.x);
                playerAnimator.SetParam(movementYParam, currentVelocitiyNormalized.z);
            }
            rollStamina += Time.deltaTime;
            rollStamina = Mathf.Clamp(rollStamina, 0, GetMaxStamina);
            ApplyMovement();
            ApplyAnimator();

            //uidebug
            UI_DEBUG.Instance.GetList[2].text = nameof(IsMoving) + IsMoving.ToString();
            UI_DEBUG.Instance.GetList[3].text = nameof(rollStamina) + rollStamina.ToString();
        }
        private void FixedUpdate()
        {
            ApplyMovement();
        }
        public void ApplyMovement()
        {
            Vector3 additionalForce = RollForce;
            Vector3 input = AllowInputMoving ? InputDirection : Vector3.zero;
            Vector3 result = velocitiy + additionalForce + input;
            result *= Time.fixedDeltaTime * speed;
            characterController.Move(result);
        }
        public void AddForce(Vector3 vector, float force = 1)
        {
            velocitiy += vector * force;
        }
        /// <summary>
        /// </summary>
        /// <param name="dashDirection">this should be normal vector</param>
        /// <param name="force">distance</param>
        public void DoABarrelRoll(Vector3 dashDirection, int force)
        {
            StopAllCoroutines();
            StartCoroutine(CO_DoABarrelRoll());
            IEnumerator CO_DoABarrelRoll()
            {
                rollStamina -= rollcost;
                AllowInputMoving = false;
                RollForce = dashDirection * force; 

                Vector3 startVelocitiy = RollForce;
                float resultDistance = GetDistance();
                float originalDistance = force;
                const float dashMultiplier = 0.3f; /* (resultDis / origianlDis) is approximately 1
                                                    * so.. = 1 * dashMultiplier; */
                float endTime = resultDistance / originalDistance * dashMultiplier;
                float timer = 0;
                Vector3 targetVector = Vector3.zero;

                float GetDistance()
                    {
                        //returns distance betwn objs when hit othewise return origianlForce(move distance)
                        float stepOffset = characterController.stepOffset;
                        Vector3 startPos = transform.position + new Vector3(0, stepOffset);

                        bool result = Physics.Raycast(startPos, dashDirection, out RaycastHit hit, force);
                        Debug.DrawRay(startPos, dashDirection * force, Color.red, 5);
                        Debug.DrawRay(startPos, Vector3.up * 5, Color.red, 5);
                        if (result)
                            Debug.DrawRay(hit.point, Vector3.up, Color.blue, 5);

                        return result
                            ? hit.distance
                            : force;
                    }
                void OnEnd()
                {
                    RollForce = targetVector;
                    AllowInputMoving = true;
                }
                while (timer < endTime)
                {
                    float curveValue = timer / endTime;
                    float val = rollCurve.Evaluate(curveValue);
                    RollForce = Vector3.Lerp(startVelocitiy, targetVector, val);
                    timer += Time.deltaTime;
                    yield return null;
                }
                OnEnd();
            }
        }
        /// <summary>
        /// </summary>
        /// <param name="dashDirection">this should be normal vector</param>
        /// <param name="force">distance</param>
        public void DoABarrelRoll(Vector3 dashDirection, int force, Action onEndCallback)
        {
            StopAllCoroutines();
            StartCoroutine(CO_DoABarrelRoll());
            IEnumerator CO_DoABarrelRoll()
            {
                rollStamina -= rollcost;
                AllowInputMoving = false;
                RollForce = dashDirection * force;

                Vector3 startVelocitiy = RollForce;
                float resultDistance = GetDistance();
                float originalDistance = force;
                const float dashMultiplier = 0.3f; /* (resultDis / origianlDis) is approximately 1
                                                    * so.. = 1 * dashMultiplier; */
                float endTime = resultDistance / originalDistance * dashMultiplier;
                float timer = 0;
                Vector3 targetVector = Vector3.zero;

                float GetDistance()
                {
                    //returns distance betwn objs when hit othewise return origianlForce(move distance)
                    float stepOffset = characterController.stepOffset;
                    Vector3 startPos = transform.position + new Vector3(0, stepOffset);

                    bool result = Physics.Raycast(startPos, dashDirection, out RaycastHit hit, force);
                    Debug.DrawRay(startPos, dashDirection * force, Color.red, 5);
                    Debug.DrawRay(startPos, Vector3.up * 5, Color.red, 5);
                    if (result)
                        Debug.DrawRay(hit.point, Vector3.up, Color.blue, 5);

                    return result
                        ? hit.distance
                        : force;
                }
                void OnEnd()
                {
                    RollForce = targetVector;
                    AllowInputMoving = true;
                    onEndCallback?.Invoke();
                }
                while (timer < endTime)
                {
                    float curveValue = timer / endTime;
                    float val = rollCurve.Evaluate(curveValue);
                    RollForce = Vector3.Lerp(startVelocitiy, targetVector, val);
                    timer += Time.deltaTime;
                    yield return null;
                }
                OnEnd();
            }
        }

    }
}

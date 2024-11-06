using CardGame.FSM;
using CardGame.FSM.States;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CardGame.Players
{
    public class Player : MonoBehaviour
    {
        private readonly Dictionary<Type, IPlayerComponent> componentDictionary = new();
        private PlayerMovement GetPlayerMovement => GetPlayerComponent<PlayerMovement>();
        private PlayerRenderer GetPlayerRenderer => GetPlayerComponent<PlayerRenderer>();
        public PlayerInput GetInput => GetPlayerComponent<PlayerInput>();
        public PlayerAnimator GetAnimator => GetPlayerComponent<PlayerAnimator>();
        public FiniteStateMachine<PlayerStateEnum.Movement, Player> PlayerFSM_Movement { get; private set; }
        public FiniteStateMachine<PlayerStateEnum.Combat,   Player> PlayerFSM_Combat { get; private set; }
        private void Awake()
        {
            void Initialize()
            {
                var componentList =
                GetComponentsInChildren<IPlayerComponent>(true).
                    ToList();
                componentList.ForEach(x =>
                {
                    InitializePlayerComponent(x);
                });
                componentList.ForEach(x =>
                {
                    if (x is IPlayerComponentStartInit result) result.StartInit(this);
                });


                //FSM
                PlayerFSM_Movement = new(this);
                PlayerFSM_Combat = new(this);

                //manualinit

                //fsmmovement
                PlayerFSM_Movement.AddState(PlayerStateEnum.Movement.None, new PS_None());
                PlayerFSM_Movement.AddState(PlayerStateEnum.Movement.Idle, new PS_Idle());
                PlayerFSM_Movement.AddState(PlayerStateEnum.Movement.Dash, new PS_Dash());

                //fsmcombat
                PlayerFSM_Combat.AddState(PlayerStateEnum.Combat.None, new PS_None());
                PlayerFSM_Combat.AddState(PlayerStateEnum.Combat.NormalAttack, new PS_NormalAttack());

                PlayerFSM_Combat.SetCurrentState(PlayerStateEnum.Combat.None);
            }
            Initialize();
        }
        private void Start()
        {
            void SetUpEvent()
            {
                var inp = GetPlayerComponent<PlayerInput>();
                inp.EventPlayerRoll += HandleOnRoll;
            }
            void SetUpInventory()
            {

                //int n = 2;//capacitiy of inventory
                //int cnt = 0;
                var inv = GetPlayerComponent<PlayerInventory>();
                //foreach (var item in cardSO)
                //{
                //    if (cnt >= n) break;
                //    inv.AddInventory(item);
                //    cnt++;
                //}
            }
            SetUpEvent();
            SetUpInventory();
        }
        private void OnDestroy()
        {
            void UnSubscribeEvent()
            {
                var inp = GetPlayerComponent<PlayerInput>();
                inp.EventPlayerRoll -= HandleOnRoll;
            }
            void OnDispose()
            {
                var componentList = componentDictionary.ToList();
                componentList.ForEach(x => x.Value?.Dispose(this));
            }
            UnSubscribeEvent();
            OnDispose();
        }
        private void HandleOnRoll()
        {
            bool isMoving = GetPlayerMovement.IsMoving;
            if (isMoving)
                GetPlayerMovement.DoABarrelRoll(GetPlayerMovement.GetVelocitiy.normalized, 4);
            //playerRenderer.SetMaterial(playerRenderer.GetMatOnRoll);
        }

        private void Update()
        {
            void PlayerComponentUpdate()
            {
                void DebugInput()
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        //PlayerFSM_Combat.ChangeState();
                    }

                }
                void PlayerMovement()
                {
                    Vector3 result = GetInput.GetCameraRelativeInput;
                    GetPlayerComponent<PlayerMovement>().InputDirection = result;
                    if (Input.GetKeyDown(KeyCode.O))
                    {
                        GetPlayerMovement.AddForce(Vector3.left, 2);
                    }
                }
                void PlayerCamera()
                {

                }
                DebugInput();
                PlayerMovement();
                PlayerCamera();
            }
            PlayerComponentUpdate();
            PlayerFSM_Combat.UpdateState();
        }

        private IPlayerComponent InitializePlayerComponent(IPlayerComponent component = null)
        {
            if (component == null)
                component = GetComponentInChildren<IPlayerComponent>(true);

            componentDictionary.Add(component.GetType(), component);
            component.Init(this);
            return component;
        }
        public T GetPlayerComponent<T>() where T : Component, IPlayerComponent
        {
            if (componentDictionary.TryGetValue(typeof(T), out IPlayerComponent value))
            {
                return value as T;
            }
            Debug.LogError("[ERROR]can't find Player_Component, reInitializing...");
            return InitializePlayerComponent() as T;
        }

    }

}
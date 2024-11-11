using CardGame.FSM;
using CardGame.FSM.States;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CardGame.Players
{
    public class Player : Entity
    {
        private readonly Dictionary<Type, IPlayerComponent> componentDictionary = new();
        public FiniteStateMachine<PlayerStateEnum.Movement, Player> PlayerFSM_Movement { get; private set; }
        public FiniteStateMachine<PlayerStateEnum.Combat,   Player> PlayerFSM_Combat { get; private set; }
        [SerializeField] private PlayerSingletonSO playerSingletonSO;
        #region GetPlayerComponent
        public PlayerMovement GetPlayerMovement => GetPlayerComponent<PlayerMovement>();
        public PlayerRenderer GetPlayerRenderer => GetPlayerComponent<PlayerRenderer>();
        public PlayerInput GetInput => GetPlayerComponent<PlayerInput>();
        public PlayerAnimator GetPlayerAnimator => GetPlayerComponent<PlayerAnimator>();
        public PlayerInventory GetInventory => GetPlayerComponent<PlayerInventory>();
        #endregion
        private void Awake()
        {
            void Initialize()
            {
                playerSingletonSO.SetPlayer(this);

                var componentList =
                GetComponentsInChildren<IPlayerComponent>(true).
                    ToList();
                componentList.ForEach(x =>
                {
                    InitializePlayerComponent(x);
                });
                componentList.ForEach(x =>
                {
                    if (x is IPlayerComponentStartInit iPlayerComp) iPlayerComp.StartInit(this);
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
                OnSceneEnter.OnSceneEnterEvent += HandleOnSceneEnter;

                var inp = GetPlayerComponent<PlayerInput>();
                inp.EventPlayerRoll += HandleOnRoll;
                inp.EventPlayerAttack += HandleOnPlayerAttack;
            }

            SetUpEvent();
            //SetUpInventory();
        }

        private void HandleOnSceneEnter(SceneEnum state)
        {
            switch (state)
            {
                case SceneEnum.Scene3D:
                    SetUpInventory();
                    break;
            }
        }
        private void SetUpInventory()
        {

            //int n = 2;//capacitiy of inventory
            //int cnt = 0;
            //var inv = GetPlayerComponent<PlayerInventory>();
            //foreach (var item in cardSO)
            //{
            //    if (cnt >= n) break;
            //    inv.AddInventory(item);
            //    cnt++;
            //}
        }

        private void OnDestroy()
        {
            void UnSubscribeEvent()
            {
                OnSceneEnter.OnSceneEnterEvent += HandleOnSceneEnter;

                var inp = GetPlayerComponent<PlayerInput>();
                inp.EventPlayerRoll -= HandleOnRoll;
                inp.EventPlayerAttack -= HandleOnPlayerAttack;
            }
            void OnDispose()
            {
                var componentList = componentDictionary.ToList();
                componentList.ForEach(x => x.Value?.Dispose(this));
            }
            UnSubscribeEvent();
            OnDispose();
        }
        private void HandleOnPlayerAttack()
        {
            GetInventory.GetCurrentWeapon.TryAttack();
        }
        private void HandleOnRoll()
        {
            bool isMoving = GetInput.KeyNotPressedTime < 0.2f;//0.25~ 0.3 is safe. if legnth of dir > 0
            if (isMoving)
                GetPlayerMovement.TryDoABarrelRoll(GetInput.GetCameraRelativeInput, 6);
        }

        private void Update()
        {
            void PlayerComponentUpdate()
            {
                void DebugInput()
                {
                    if (Input.GetKeyDown(KeyCode.F1))
                    {
                        WaveManager.Instance.ChangeWave(SceneEnum.SceneDeckSelect);
                    }
                }
                void PlayerMovement()
                {
                    Vector3 result = GetInput.GetCameraRelativeInputRaw;
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

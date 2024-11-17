using CardGame.FSM;
using CardGame.FSM.States;
using CardGame.GameEvent;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CardGame.Players
{
    public class Player : Entity
    {
        [Header("Audio")]
        [SerializeField] private AudioEmitter audioEmitterOnHurt;
        [SerializeField] private AudioEmitter audioEmitterOnMeleeHit;
        [SerializeField] private AudioEmitterRandom audioEmitterSwing;

        [Header("Animation Param")]
        [SerializeField] private AnimationParameterSO normalAttackParam;
        [SerializeField] private AnimationParameterSO specialAttackParam;

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
                PlayerFSM_Movement = new(this, GetPlayerAnimator);
                PlayerFSM_Combat = new(this, GetPlayerAnimator);

                //manualinit

                //fsmmovement
                PlayerFSM_Movement.AddState(PlayerStateEnum.Movement.Idle, new PS_Idle(null));
                PlayerFSM_Movement.AddState(PlayerStateEnum.Movement.Dash, new PS_Dash(null));

                //fsmcombat
                PlayerFSM_Combat.AddState(PlayerStateEnum.Combat.None, new PS_None(null));
                PlayerFSM_Combat.AddState(PlayerStateEnum.Combat.NormalAttack, new PS_NormalAttack(normalAttackParam));
                PlayerFSM_Combat.AddState(PlayerStateEnum.Combat.SpecialAttack, new PS_SpecialAttack(specialAttackParam));

                PlayerFSM_Combat.SetCurrentState(PlayerStateEnum.Combat.None);
            }
            Initialize();
            playerSingletonSO.SetPlayer(this, GetPlayerMovement.transform);
        }
        private void Start()
        {
            void SetUpEvent()
            {
                PlayerHealth.OnHitEvent += HandleOnPlayerHit;
                var inp = GetInput;
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
                PlayerHealth.OnHitEvent -= HandleOnPlayerHit;
                var inp = GetInput;
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
            bool result = GetInventory.GetCurrentWeapon.TryAttack();
            if (result)
            {
                audioEmitterSwing.PlayAudio();
            }
        }
        private void HandleOnRoll()
        {
            bool isMoving = GetInput.KeyNotPressedTime < 0.2f;//0.25~ 0.3 is safe. if legnth of dir > 0
            if (isMoving)
                GetPlayerMovement.TryDoABarrelRoll(GetInput.GetCameraRelativeInput, 6);
        }
        private void HandleOnPlayerHit()
        {
            audioEmitterOnHurt.PlayAudio();
        }
        private void Update()
        {
            void PlayerComponentUpdate()
            {
                void DebugInput()
                {
                    if (Input.GetKeyDown(KeyCode.F1))
                    {
                        PlayerFSM_Combat.ChangeState(PlayerStateEnum.Combat.NormalAttack);
                    }
                    if (Input.GetKeyDown(KeyCode.F2))
                    {
                        PlayerFSM_Combat.ChangeState(PlayerStateEnum.Combat.SpecialAttack);
                    }
                    if(Input.GetKeyDown(KeyCode.Q))
                    {
                        var l = GetInventory.GetSkills;
                        l[0].UseSkill(this);
                    }
                    UI_DEBUG.Instance.GetList[4].text = nameof(PlayerFSM_Combat) + PlayerFSM_Combat.CurrentState;
                }
                void PlayerMovement()
                {
                    Vector3 result = GetInput.GetCameraRelativeInputRaw;
                    if (result.sqrMagnitude > 0)
                        GetPlayerRenderer.SetVisualDirection(result);
                    GetPlayerMovement.InputDirection = result;

                    //if (Input.GetKeyDown(KeyCode.O))
                    //{
                    //    GetPlayerMovement.AddForce(Vector3.left, 2);
                    //}

                }
                void PlayerCamera()
                {

                }
                void PlayerUI()
                {
                    float maxStamina = GetPlayerMovement.GetMaxStamina;
                    float currentStamina = GetPlayerMovement.GetCurrentStamina;
                    UI_Player.Instance.GetList[0].text = $"STAMINA : {currentStamina:F1} | {maxStamina}";

                    float maxHp = default;
                    float currentHp = default;
                    UI_Player.Instance.GetList[1].text = $"HP : {currentHp:F1} | {maxHp}";
                }
                DebugInput();
                PlayerMovement();
                PlayerCamera();
                PlayerUI();
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

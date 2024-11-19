using CardGame.FSM;
using CardGame.FSM.States;
using System;
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
        [SerializeField] private AudioEmitterRandom audioEmitterDash;

        [Header("Animation Param")]
        [SerializeField] private AnimationParameterSO normalAttackParam;
        [SerializeField] private AnimationParameterSO specialAttackParam;

        [Space(10)]
        [SerializeField] private AnimationParameterSO idleAnimationParam;
        [SerializeField] private AnimationParameterSO runningAnimationParam;

        private readonly Dictionary<Type, IPlayerComponent> componentDictionary = new();
        public FiniteStateMachine<PlayerStateEnum.Movement, Player> PlayerFSM_Movement { get; private set; }
        public FiniteStateMachine<PlayerStateEnum.Combat, Player> PlayerFSM_Combat { get; private set; }
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
                PS_Base<PlayerStateEnum.Movement, Player>.StateMachine = PlayerFSM_Movement;
                PS_Base<PlayerStateEnum.Movement, Player>.BaseOwner = this;

                PlayerFSM_Combat = new(this, GetPlayerAnimator);
                PS_Base<PlayerStateEnum.Combat, Player>.StateMachine = PlayerFSM_Combat;
                PS_Base<PlayerStateEnum.Combat, Player>.BaseOwner = this;

                //states
                //fsmmovement
                PlayerFSM_Movement.AddState(PlayerStateEnum.Movement.Idle, new PS_Idle(idleAnimationParam));
                PlayerFSM_Movement.AddState(PlayerStateEnum.Movement.Running, new PS_Running(runningAnimationParam));
                PlayerFSM_Movement.AddState(PlayerStateEnum.Movement.Roll, new PS_Roll(null));

                PlayerFSM_Movement.SetCurrentState(PlayerStateEnum.Movement.Idle);

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
            void SubscribeEvent()
            {
                PlayerHealth.OnHitEvent += HandleOnPlayerHit;
                var inp = GetInput;
                PS_Roll.OnRoll += HandleOnRoll;
                inp.EventPlayerAttack += HandleOnPlayerAttack;
            }
            SubscribeEvent();
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
                PS_Roll.OnRoll -= HandleOnRoll;
                inp.EventPlayerAttack -= HandleOnPlayerAttack;
            }
            void OnComponentDispose()
            {
                var componentList = componentDictionary.ToList();
                componentList.ForEach(x => x.Value?.Dispose(this));
            }
            UnSubscribeEvent();
            OnComponentDispose();
        }
        private void HandleOnRoll()
        {
            audioEmitterDash.PlayAudio();
        }

        private void HandleOnPlayerAttack()
        {
            bool result = GetInventory.GetCurrentWeapon.TryAttack();
            if (result)
            {
                audioEmitterSwing.PlayAudio();
            }
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
                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        var l = GetInventory.GetSkills;
                        l[0].UseSkill(this);
                    }
                    UI_DEBUG.Instance.GetList[4].text = nameof(PlayerFSM_Combat) + PlayerFSM_Combat.CurrentState;
                    UI_DEBUG.Instance.GetList[5].text = nameof(PlayerFSM_Movement) + PlayerFSM_Movement.CurrentState;
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
                PlayerUI();
            }
            PlayerComponentUpdate();
            PlayerFSM_Combat.UpdateState();
            PlayerFSM_Movement.UpdateState();
        }
        private void FixedUpdate()
        {
            PlayerFSM_Combat.FixedUpdateState();
            PlayerFSM_Movement.FixedUpdateState();
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

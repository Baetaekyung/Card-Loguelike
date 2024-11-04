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
        
        private void Awake()
        {
            #region LocalFunctionDeclare
            void Initialize()
            {
                GetComponentsInChildren<IPlayerComponent>(true).
                    ToList().
                    ForEach(x => InitializePlayerComponent(x));
            }
            #endregion
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
            bool isPressingAnyDirectionKey = GetInput.InputMovementDirection.sqrMagnitude > 0;
            if (isPressingAnyDirectionKey)
                GetPlayerMovement.DoABarrelRoll(GetInput.GetCameraRelativeInput, 4);
            //playerRenderer.SetMaterial(playerRenderer.GetMatOnRoll);
        }

        private void Update()
        {
            void PlayerComponentUpdate()
            {
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
                PlayerMovement();
                PlayerCamera();
            }
            PlayerComponentUpdate();
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

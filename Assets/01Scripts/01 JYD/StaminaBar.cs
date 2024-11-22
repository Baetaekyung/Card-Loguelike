using System;
using CardGame.Players;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace CardGame
{
    public class StaminaBar : MonoBehaviour
    {
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private Image staminaBar;
        [SerializeField] private PlayerSingletonSO PlayerSingletonSo;

        void Update()
        {
            if (playerMovement == null)
            {
                playerMovement = PlayerSingletonSo.PlayerTransform.GetComponentInChildren<PlayerMovement>();
            }
            
            staminaBar.fillAmount = playerMovement.GetCurrentStamina / playerMovement.GetMaxStamina;
        }
    }
}

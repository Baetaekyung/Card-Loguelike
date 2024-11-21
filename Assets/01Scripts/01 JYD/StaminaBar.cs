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
        
        void Update()
        {
            staminaBar.fillAmount = playerMovement.GetCurrentStamina / playerMovement.GetMaxStamina;
        }
    }
}

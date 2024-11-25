using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace CardGame
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;

        public GameObject titleUI;
        public GameObject inGameBattleUI;
        public GameObject deckSelectUI;
        public GameObject settingPanelUI;
         public GameObject fadePanelUI;

        public HealthBar HealthBar;
        public StaminaBar StaminaBar;

        public TextMeshProUGUI waveText;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }

        public void SetTitleUIActive(bool isActive)
        {
            titleUI.SetActive(isActive);
        }

        public void SetInGameBattleUIActive(bool isActive)
        {
            inGameBattleUI.SetActive(isActive);
        }

        public void SetDeckSelectActive(bool isActive)
        {
            deckSelectUI.SetActive(isActive);
        }

        public void SetFadePanelActive(bool isActive)
        {
            fadePanelUI.SetActive(isActive);
        }

        public void SetSettingPanelUIActive(bool isActive)
        {
            if(isActive)
            {
                PopUpManager.Instance.SetSettingPanelTrue();
            }
            else
            {
                PopUpManager.Instance.SetSettingPanelFalse();
            }
        }
    }
}

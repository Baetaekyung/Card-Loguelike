using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace CardGame
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;

        public GameObject inGameBattleUI;
        public GameObject deckSelectUI;
        public GameObject settingPanelUI;
        [FormerlySerializedAs("FadePanelUI")] public GameObject fadePanelUI;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
            {
                Destroy(gameObject);
            }
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
            settingPanelUI.SetActive(isActive);
        }
    }
}

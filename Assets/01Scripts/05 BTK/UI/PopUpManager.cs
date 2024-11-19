using UnityEngine;
using UnityEngine.InputSystem;
using CardGame;

namespace CardGame
{
    public class PopUpManager : MonoBehaviour
    {
        public static PopUpManager Instance { get; private set; }

        private CanvasGroup _settingPanel;
        public bool IsSetting { get; set; }

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        private void Start()
        {
            IsSetting = false;
            _settingPanel = FindAnyObjectByType<SettingPanel>().GetComponent<CanvasGroup>();
            _settingPanel.alpha = 0f;
        }

        private void Update()
        {
            if(Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                IsSetting = !IsSetting;
                SetActivePanel(IsSetting);
            }
        }

        private void SetActivePanel(bool isActive)
        {
            _settingPanel.alpha = isActive ? 1f : 0f;
        }

        public void SetSettingPanelFalse()
        {
            IsSetting = false;
            SetActivePanel(false);
        }
    }
}

using TMPro;
using UnityEngine;

namespace CardGame
{
    public class WindowModeDropdown : MonoBehaviour
    {
        public static bool windowMode = true;

        [SerializeField] private TMP_Dropdown _dropdown;

        private void Start()
        {
            _dropdown.onValueChanged.AddListener((int cnt) =>
            {
                if(cnt == 0)
                {
                    windowMode = true;
                    Screen.SetResolution(Screen.width, Screen.height, windowMode);
                    Debug.Log("FullScreen");
                }
                else if(cnt == 1)
                {
                    windowMode = false;
                    Screen.SetResolution(Screen.width, Screen.height, windowMode);
                    Debug.Log("Window Mode");
                }
            });
        }
    }
}

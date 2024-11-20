using TMPro;
using UnityEngine;

namespace CardGame
{
    public class ResolutionDropdown : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown _dropdown;

        private void Start()
        {
            _dropdown.onValueChanged.AddListener((int cnt) =>
            {
                SetResolution(cnt);
            });
        }

        private void SetResolution(int cnt)
        {
            if (cnt == 0)
            {
                Screen.SetResolution(1920, 1080, WindowModeDropdown.windowMode);
                Debug.Log($"Set resolution to 1920 * 1080 {WindowModeDropdown.windowMode}");
            }
            else if (cnt == 1)
            {
                Screen.SetResolution(2520, 1440, WindowModeDropdown.windowMode);
                Debug.Log($"Set resolution to 2520 * 1440 {WindowModeDropdown.windowMode}");
            }
            else if (cnt == 2)
            {
                Screen.SetResolution(1366, 768, WindowModeDropdown.windowMode);
                Debug.Log($"Set resolution to 1366 * 768 {WindowModeDropdown.windowMode}");
            }
        }
    }
}

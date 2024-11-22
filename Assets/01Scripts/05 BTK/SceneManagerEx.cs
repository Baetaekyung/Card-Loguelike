using UnityEngine;
using UnityEngine.SceneManagement;

namespace CardGame
{
    [MonoSingletonUsage(MonoSingletonFlags.DontDestroyOnLoad)]
    public class SceneManagerEx : MonoSingleton<SceneManagerEx>
    {
        private UIManager uiManager => UIManager.Instance;

        public void ChangeScene(SceneEnum sceneEnum)
        {
            switch (sceneEnum)
            {
                case SceneEnum.TitleScene:
                    uiManager.SetTitleUIActive(true);
                    uiManager.SetSettingPanelUIActive(false);
                    uiManager.SetDeckSelectActive(false);
                    uiManager.SetFadePanelActive(true);
                    uiManager.SetInGameBattleUIActive(false);
                    SceneManager.LoadScene("TitleScene");
                    break;
                case SceneEnum.SceneDeckSelect:
                    uiManager.SetTitleUIActive(false);
                    uiManager.SetSettingPanelUIActive(false);
                    uiManager.SetDeckSelectActive(true);
                    uiManager.SetFadePanelActive(false);
                    uiManager.SetInGameBattleUIActive(false);
                    SceneManager.LoadScene("CardUI Updated");
                    break;
                case SceneEnum.Scene3D:
                    uiManager.SetTitleUIActive(false);
                    uiManager.SetSettingPanelUIActive(false);
                    uiManager.SetDeckSelectActive(false);
                    uiManager.SetFadePanelActive(false);
                    uiManager.SetInGameBattleUIActive(true);
                    SceneManager.LoadScene("Map_1 1");
                    break;
            }
        }
    }
}

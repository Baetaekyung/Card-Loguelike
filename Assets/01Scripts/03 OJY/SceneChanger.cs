using UnityEngine;

namespace CardGame
{
    public class SceneChanger : MonoBehaviour
    {
        public void RE_ChangeScne(string name)
        {
            WaveManager.Instance.ChangeWave(SceneEnum.Scene3D);
        }
    }
}

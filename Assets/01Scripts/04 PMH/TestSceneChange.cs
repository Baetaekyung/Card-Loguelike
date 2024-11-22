using UnityEngine;
using UnityEngine.SceneManagement;

namespace CardGame
{
    public class TestSceneChange : MonoBehaviour
    {
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.L))
            {
                SceneManager.LoadScene("CardUI Updated");
            }
            
        }
    }
}

using UnityEngine;

namespace CardGame
{
    public class FadePanel : MonoBehaviour
    {
        private Animator _animator;
        private string _nextSceneName;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void FadeIn(string nextSceneName)
        {
            _animator.Play("FadeIn");
            _nextSceneName = nextSceneName;
        }

        public void FadeOut()
        {
            _animator.Play("FadeOut");
        }

        public void LoadNextScene()
        {
            Debug.Log($"Load {_nextSceneName} Scene!");
        }
    }
}

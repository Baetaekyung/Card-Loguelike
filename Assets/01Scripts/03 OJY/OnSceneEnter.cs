using System;
using UnityEngine;

namespace CardGame
{
    [DefaultExecutionOrder(-100)]
    public class OnSceneEnter : MonoBehaviour
    {
        [SerializeField] private SceneEnum currentScene;
        /// <summary>
        /// Subscribe this on awake only
        /// </summary>
        public static event Action<SceneEnum> OnSceneEnterEvent;
        public static SceneEnum CurrentScene { get; private set; }
        private void Start()
        {
            CurrentScene = currentScene;
            OnSceneEnterEvent?.Invoke(CurrentScene);
        }
    }
}

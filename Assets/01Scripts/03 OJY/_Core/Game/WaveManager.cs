using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace CardGame
{
    public enum SceneEnum
    {
        SceneDeckSelect,
        Scene3D
    }

    [MonoSingletonUsage(MonoSingletonFlags.DontDestroyOnLoad)]
    public class WaveManager : MonoSingleton<WaveManager>
    {
        public int CurrentWave { get; private set; }
        public event Action<SceneEnum> OnWaveChanged;
        protected override void Awake()
        {
            base.Awake();
            OnWaveChanged += HandleOnWaveChanged;
            //OnSceneEnter.OnSceneEnterEvent += HandleOnSceneEnter;
        }

        private void Start()
        {
            EnemySpawnManager.Instance.SpawnEnemy(CurrentWave);

        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            OnWaveChanged -= HandleOnWaveChanged;
            //OnSceneEnter.OnSceneEnterEvent -= HandleOnSceneEnter;
        }
        private void HandleOnSceneEnter(SceneEnum obj)
        {
            void OnScene3D()
            {
                CurrentWave++;
                UI_Wave.Instance.GetList[0].text = "CurrentWave : " + CurrentWave;
                UI_Wave.Instance.GetList[1].text = "S           : " + SkillManager.Instance.registerSkills.Count;
            }
            switch (obj)
            {
                case SceneEnum.SceneDeckSelect:
                    UIManager.Instance.SetDeckSelectActive(true);
                    UIManager.Instance.SetSettingPanelUIActive(false);
                    UIManager.Instance.SetFadePanelActive(false);
                    break;
                case SceneEnum.Scene3D:
                    OnScene3D();
                    break;

            }
        }
        private void HandleOnWaveChanged(SceneEnum waveState)
        {
            switch (waveState)
            {
                case SceneEnum.SceneDeckSelect:
                    OnDeckSelectScene();
                    break;
                case SceneEnum.Scene3D:
                    On3DScene();
                    break;
            }
        }
        public void ChangeWave(SceneEnum waveState)
        {
            OnWaveChanged?.Invoke(waveState);
        }

        private void OnDeckSelectScene()
        {
            LoadScene("CardUI Updated");
        }
        private void On3DScene()
        {
            LoadScene("Map_1 1");
        }
        private void LoadScene(string name)
        {
            SceneManager.LoadScene(name);
        }
    }
}

using CardGame.Players;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CardGame
{
    [MonoSingletonUsage(MonoSingletonFlags.DontDestroyOnLoad)]
    public class SkillManager : MonoSingleton<SkillManager>
    {
        public Player player;
        public List<BaseSkill> registerSkills = new();
        public IList<BaseSkill> GetSkills => registerSkills;
        private BaseSkill _currentSkill;

        public event Action OnSkillRegisted;

        private int _idx = 0;
        protected override void Awake()
        {
            base.Awake();
            OnSceneEnter.OnSceneEnterEvent += HandleOnSceneEnter;
        }

        private void HandleOnSceneEnter(SceneEnum obj)
        {
            switch (obj)
            {
                case SceneEnum.SceneDeckSelect:
                    registerSkills.Clear();
                    break;
            }
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            OnSceneEnter.OnSceneEnterEvent -= HandleOnSceneEnter;
        }

        private void Update()
        {
            if (registerSkills.Count == 0) return;

            ChangeCurrentSkill();
        }

        public void RegistSkill(BaseSkill skill)
        {
            if (registerSkills.Count == 6)
            {
                Debug.Log("Skill is full");
                return;
            }

            registerSkills.Add(skill);

            OnSkillRegisted?.Invoke();
        }

        private void RegisterCurrentSkill()
        {
            _currentSkill = registerSkills[_idx];
        }

        public void UseSkill()
        {
            _currentSkill.UseSkill(player);
        }

        private void ChangeCurrentSkill()
        {
            float wheelInput = Mouse.current.scroll.y.ReadValue();

            if (wheelInput < 0)
                _idx = (_idx - 1) < 0 ? registerSkills.Count - 1 : _idx - 1;
            else if (wheelInput > 0)
                _idx = (_idx + 1) > registerSkills.Count - 1 ? 0 : _idx + 1;


            RegisterCurrentSkill();
        }
    }
}

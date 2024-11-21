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
        public PlayerSingletonSO playerSingletonSO;
        public List<BaseSkill> registerSkills = new();
        public SkillSlot[] inGameUIs;
        public IList<BaseSkill> GetSkills => registerSkills;
        private BaseSkill _currentSkill;

        public event Action OnSkillRegisted;

        public int _idx = 0;

        private GameObject game;
        private void Start()
        {
            registerSkills.ForEach((skill) =>
            {
                skill.AWDlpawdakdpadoawkakwodpkdopwkdakWOdkowaijdiwadjiadwajdiwadwioajdaiodjawdjioajdiawdajddjnjdkjlkjngjjkjkgjkkbkcxkjkxfjkggjggkggkggjkggkgkggkgkgjgkgjxzlxkmbkcmkmvklcjbxklxjbjkjcixjbpokrjriyjeiitjgifdfgdkgjkdfglndhdfkd();
                print("reseting");
            });
        }

        private void Update()
        {
            if (registerSkills.Count == 0)
            {
                foreach(var item in inGameUIs)
                {
                    item.cG.alpha = 0;
                }
                return;
            }

            ChangeCurrentSkill();
        }

        public void RegistSkill(BaseSkill skill)
        {
            print("register skill");
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
            _currentSkill.TryUseSkill(playerSingletonSO.Instance);
        }

        private void ChangeCurrentSkill()
        {
            int temp = 0;
            for(int i = 0; i < registerSkills.Count; i++)
            {
                if (i == _idx)
                {
                    inGameUIs[i].cG.alpha = 1f;
                }
                else
                {
                    inGameUIs[i].cG.alpha = 0.5f;
                }
                inGameUIs[i].SetSkillImage(registerSkills[i].SkillImage);
                
                temp++;
            }

            for(int j = temp; j < inGameUIs.Length; j++)
            {
                inGameUIs[j].cG.alpha = 0f;
            }
            
            float wheelInput = Mouse.current.scroll.y.ReadValue();

            if (wheelInput < 0)
                _idx = (_idx - 1) < 0 ? registerSkills.Count - 1 : _idx - 1;
            else if (wheelInput > 0)
                _idx = (_idx + 1) > registerSkills.Count - 1 ? 0 : _idx + 1;
            

            RegisterCurrentSkill();
        }
    }
}

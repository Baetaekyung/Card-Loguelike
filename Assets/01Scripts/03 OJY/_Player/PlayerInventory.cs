using CardGame.Weapons;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CardGame.Players
{
    public class PlayerInventory : MonoBehaviour, IPlayerComponent, IPlayerComponentStartInit
    {
        [Header("Weapons")]
        [Tooltip("Dont Edit this on inspector")]
        [SerializeField] private List<BaseWeapon> weapons;
        private int currentWeaponIndex;
        public BaseWeapon GetCurrentWeapon => weapons[currentWeaponIndex];

        [Header("Skills")]
        [Tooltip("Dont Edit this on inspector")]
        [SerializeField] private List<BaseSkill> skills;
        public IList<BaseSkill> GetSkills => skills;

        public void Init(Player _player)
        {
            if(weapons.Count == 0)
                weapons = GetComponentsInChildren<BaseWeapon>(true).ToList();
        }
        public void StartInit(Player _player)
        {
            skills = SkillManager.Instance.GetSkills.ToList();
            AnimationEventTrigger.OnAttackEventTrigger += HandleOnAttackTrigger;
        }

        private void HandleOnAttackTrigger()
        {
            GetCurrentWeapon.OnAnimatoinEventTrigger();
        }

        public void Dispose(Player _player)
        {
            AnimationEventTrigger.OnAttackEventTrigger -= HandleOnAttackTrigger;
        }


    }

}

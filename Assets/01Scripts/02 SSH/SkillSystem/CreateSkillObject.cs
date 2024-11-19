using System;
using CardGame.Players;
using UnityEngine;

namespace CardGame
{
    public class CreateSkillObject : BaseSkill
    {
        [SerializeField] private GameObject SkillObject;
        
        [SerializeField] private float _cooltime;
        private float _lastUsedTime;


        public override void ResetSkill()
        {
            _lastUsedTime = 0f;
            Debug.Log("lastusedtime reseted");
        }

        public override void UseSkill(Player owner)
        {
            if (_lastUsedTime + _cooltime < Time.time)
            {
                _lastUsedTime = Time.time;
                Instantiate(SkillObject, owner.transform.GetChild(0).transform.position, Quaternion.identity);
            }
        }
    }
}

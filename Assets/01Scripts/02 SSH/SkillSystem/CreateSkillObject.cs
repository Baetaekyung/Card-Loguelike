using CardGame.Players;
using UnityEngine;

namespace CardGame
{
    public class CreateSkillObject : BaseSkill
    {
        [SerializeField] private GameObject SkillObject;
        
        [SerializeField] private float _cooltime;
        private float _lastUsedTime =0f;


        public override void UseSkill(Player owner)
        {
            if (_lastUsedTime + _cooltime > Time.time) return;
            _lastUsedTime = Time.time;
            Instantiate(SkillObject, owner.transform.GetChild(0).transform.position, Quaternion.identity);
        }
    }
}

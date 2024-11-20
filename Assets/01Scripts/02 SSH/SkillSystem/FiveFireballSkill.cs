using System;
using System.Collections;
using CardGame.Players;
using UnityEngine;

namespace CardGame.SSH
{
    public class FiveFireballSkill : BaseSkill
    {
        private int damage;
        [SerializeField] private GameObject Fireball;
        private Player _owner;
        private float _lastUsedTime =0f;
        private float _cooltime = 1f;

        public override void ResetSkill()
        {
            Debug.Log($"lastusedtime resetted as {_lastUsedTime}");
            _lastUsedTime =0f;
        }

        public override void UseSkill(Player owner)
        {
            print("asdf");
            _owner = owner;
            if (_lastUsedTime + _cooltime > Time.time) return;
            _lastUsedTime = Time.time;
            print("used skill" + _owner.name);
            Instantiate(Fireball, _owner.transform.GetChild(0).transform.position + Vector3.up * 5, Quaternion.identity);
        }
    }
}

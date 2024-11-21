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


        protected override void UseSkill(Player owner)
        {
            print("asdf");
            _owner = owner;
            print("used skill" + _owner.name);
            Instantiate(Fireball, _owner.transform.GetChild(0).transform.position + Vector3.up * 5, Quaternion.identity);
        }
    }
}

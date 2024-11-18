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

        private bool isCooltime = false;
        public override void UseSkill(Player owner)
        {
            if (isCooltime)return;
            isCooltime = true;
            _owner = owner;
            
            SpawnFireballs();
            Invoke("SpawnFireballs", 0.5f);
            Invoke("SpawnFireballs", 1f);
            Invoke("SpawnFireballs", 1.5f);
            Invoke("SpawnFireballs", 2f);
        }

        private void SpawnFireballs()
        {
            Instantiate(Fireball, _owner.transform.GetChild(0).transform.position + Vector3.up * 5, Quaternion.identity);
        }
    }
}

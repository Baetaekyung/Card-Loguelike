using System;
using System.Collections;
using CardGame.Players;
using UnityEngine;

namespace CardGame
{
    public class FiveFireballSkill : BaseSkill
    {
        private int damage;
        [SerializeField] private GameObject Fireball;

        private void Start()
        {
            UseSkill(null);
        }

        public override void UseSkill(Player owner)
        {
            StartCoroutine(SpawnFireballs());
        }

        private IEnumerator SpawnFireballs()
        {
            int count = 5;
            while (count-->0)
            {
                Instantiate(Fireball, transform.position + Vector3.up * 2, Quaternion.identity);
                print("fireball generated");
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}

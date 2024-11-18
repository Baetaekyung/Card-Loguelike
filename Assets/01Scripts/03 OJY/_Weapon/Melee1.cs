using UnityEngine;

namespace CardGame.Weapons
{
    public class Melee1 : BaseMelee
    {
        [SerializeField] private float distance;
        protected override void Attack()
        {
            damageCaster.Cast();
        }

    }
}

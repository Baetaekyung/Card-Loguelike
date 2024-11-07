using UnityEngine;

namespace CardGame.Weapons
{
    public class Melee1 : BaseMelee
    {
        protected override void Attack()
        {

        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            for (int i = 0; i < collisionTransforms.Count; i++)
            {
                Vector3 pos = collisionTransforms[i].position;
                Gizmos.DrawWireSphere(pos, 0.5f);
            }
        }
    }
}

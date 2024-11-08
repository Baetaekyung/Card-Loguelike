using UnityEngine;

namespace CardGame
{
    public class SphereDamageCastNonAlloc : DamageCaster
    {
        [SerializeField] private float radius;
        [SerializeField] private int maxAlloc;
        public override bool Cast(float distance = 0)
        {
            if (distance == 0) distance = defaultDistance;
            
            RaycastHit[] hits = new RaycastHit[maxAlloc];
            int result = Physics.SphereCastNonAlloc(transform.position, radius, transform.forward, hits, distance, includeLayers);
            bool isHit = result > 0;
            if (isHit)
            {
                for (int i = 0; i < result; i++)
                {
                    Transform trm = hits[i].transform;
                    int damage = 1;
                    if (trm.TryGetComponent(out IDamageable damageable))
                    {
                        print("Damage");
                        damageable.TakeDamage(damage);
                    }
                }
            }
            return isHit;
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radius);
            Gizmos.DrawWireSphere(transform.position + transform.forward * defaultDistance, radius);
        }
    }
}

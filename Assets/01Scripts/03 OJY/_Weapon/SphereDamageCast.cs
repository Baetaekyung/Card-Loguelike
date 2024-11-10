using UnityEngine;

namespace CardGame
{
    public class SphereDamageCast : DamageCaster
    {
        [Header("Settings")]
        [SerializeField] private float radius;
        public override bool Cast(float distance = 0)
        {
            if (distance == 0) distance = defaultDistance;
            bool result = Physics.SphereCast(transform.position, radius, transform.forward, out RaycastHit hit, distance, includeLayers);
            if (result)
            {
                if (hit.transform.TryGetComponent(out IDamageable compo))
                {
                    print("Damage");
                    int damage = 1;
                    compo.TakeDamage(damage);
                }
            }
            return result;
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radius);
            Gizmos.DrawWireSphere(transform.position + transform.forward * defaultDistance, radius);
        }
    }
}

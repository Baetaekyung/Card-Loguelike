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
            
            bool result = Physics.SphereCast(GetStartPosition() , radius, transform.forward * distance, out RaycastHit hit, distance, includeLayers);
            if (result)
            {
                if (hit.transform.TryGetComponent(out IDamageable compo))
                {
                    int damage = 1;

                    ActionData actionData = new ActionData();
                    
                    actionData.hitPoint = hit.point;    
                    actionData.hitPoint = hit.normal;   
                    actionData.damageAmount = damage;       
                    actionData.knockBackPower = damage;     
                    
                    
                    compo.TakeDamage(actionData);
                }
            }
            return result;
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(GetStartPosition() , radius);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(GetStartPosition() + transform.forward * defaultDistance, radius);
        }
    }
}

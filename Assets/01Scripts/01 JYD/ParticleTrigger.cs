using System.Collections.Generic;
using Unity.AppUI.Editor;
using UnityEngine;

namespace CardGame
{
    public class ParticleTrigger : MonoBehaviour
    {
        [SerializeField] protected float damageAmount;
        
        protected virtual void OnParticleCollision(GameObject other)
        {
            if (other.TryGetComponent(out IDamageable compo))
            {
                List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
                int eventCount = ParticlePhysicsExtensions.GetCollisionEvents(GetComponent<ParticleSystem>(), other, collisionEvents);

                if (eventCount > 0)
                {
                    Vector3 hitPoint = collisionEvents[0].intersection;
                    Vector3 hitNormal = collisionEvents[0].normal;
                    
                    ActionData actionData = new ActionData();
                    
                    actionData.hitPoint = hitPoint;
                    actionData.hitNormal = hitNormal;
                    
                    actionData.damageAmount = damageAmount;
                    actionData.knockBackPower = 0;
                    
                    
                    compo.TakeDamage(actionData);
                }
            }
           
        }
    }
}

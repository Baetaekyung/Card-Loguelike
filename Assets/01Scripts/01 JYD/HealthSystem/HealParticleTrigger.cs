using System.Collections.Generic;
using UnityEngine;

namespace CardGame
{
    public class HealParticleTrigger : ParticleTrigger
    {
        protected override void OnParticleCollision(GameObject other)
        {
            if (other.TryGetComponent(out IDamageable compo))
            {
                List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
                int eventCount = ParticlePhysicsExtensions.GetCollisionEvents(GetComponent<ParticleSystem>(), other, collisionEvents);

                if (eventCount > 0)
                {
                    compo.Heal(damageAmount);
                }
            }
        }
    }
}

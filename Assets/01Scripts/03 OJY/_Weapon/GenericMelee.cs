using CardGame.GameEvent;
using UnityEngine;

namespace CardGame.Weapons
{
    public class GenericMelee : BaseMelee
    {
        [SerializeField] private float distance;
        [Range(0, 2)] [SerializeField] private float shakeForce;
        public override void OnAnimatoinEventTrigger()
        {
            //put some generic stuff here
            bool result = damageCaster.Cast();
            print("cast");
            if (result)
            {
                var evtCamera = Events<EventCameraShake>.Instance;
                evtCamera.impulse = shakeForce;
                EventManager.Invoke(evtCamera);
                EventManager.Invoke(Events<EventOnEnemyHit>.Instance);
            }
        }
        
    }
}

using CardGame.GameEvent;
using UnityEngine;

namespace CardGame.Weapons
{
    public class GenericMelee : BaseMelee
    {
        [SerializeField] private float distance;
        public override void OnAnimatoinEventTrigger()
        {
            //put some generic stuff here
            bool result = damageCaster.Cast();
            print("cast");
            if (result)
            {
                //var evtCamera = Events<EventCameraShake>.Instance;
                //evtCamera.impulse = 1;
                //EventManager.Invoke(evtCamera);
                EventManager.Invoke(Events<EventOnEnemyHit>.Instance);
            }
        }
        
    }
}

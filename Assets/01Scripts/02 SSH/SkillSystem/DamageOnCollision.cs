using UnityEngine;

namespace CardGame
{
    public class DamageOnCollision : MonoBehaviour
    {
        [SerializeField] private ActionData _data;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(_data);
            }

        }
    
    }
}

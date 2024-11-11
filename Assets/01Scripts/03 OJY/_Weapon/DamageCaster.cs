using UnityEngine;

namespace CardGame
{
    public abstract class DamageCaster : MonoBehaviour
    {
        [SerializeField] protected LayerMask includeLayers = Physics.AllLayers;
        [SerializeField] protected float defaultDistance = 1f;
        public abstract bool Cast(float distance = 0);
    }
}

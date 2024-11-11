using UnityEngine;

namespace CardGame
{
    public abstract class DamageCaster : MonoBehaviour
    {
        [SerializeField] protected LayerMask includeLayers = Physics.AllLayers;
        [SerializeField] protected float defaultDistance = 1f;
        [SerializeField] protected float _casterInterpolation;
        public abstract bool Cast(float distance = 0);
        
        public Vector3 GetStartPosition()
        {
            return transform.position + transform.forward * -_casterInterpolation * 2; 
        }
    }
}

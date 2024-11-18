using System.Collections.Generic;
using UnityEngine;

namespace CardGame
{
    public class Pizza : MonoBehaviour
    {
        [SerializeField] private List<Transform> collisionTransforms;
        [SerializeField] private float range;
        
        private void OnDrawGizmos()
        {
            if (collisionTransforms.Count == 0) return;
            Gizmos.color = Color.red;
            for (int i = 0; i < collisionTransforms.Count; i++)
            {
                Transform trm = collisionTransforms[i];
                Vector3 forward = trm.forward;
                Vector3 pos = trm.position;
                Gizmos.DrawRay(pos, forward * range);
            }
        }

    }
}
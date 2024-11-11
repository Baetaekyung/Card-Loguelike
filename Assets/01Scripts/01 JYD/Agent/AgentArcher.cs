using Unity.Mathematics;
using UnityEngine;

namespace CardGame
{
    public class AgentArcher : Agent
    {
        [SerializeField] private Transform firePos;
        [SerializeField] private float firePower;
        
        [SerializeField] private Projectile arrow;

        [SerializeField] private GameObject arrowObj;
        
        private void FireArrow()
        {
            Projectile p = Instantiate(arrow, firePos.position ,Quaternion.identity);
            p.Shot(firePos.forward , firePower);
        }

        private void SetActiveArrowObj(int idx)//1 is true 0 is false
        {
            arrowObj.SetActive(idx == 1);
        }
    }
}

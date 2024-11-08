using UnityEngine;

namespace CardGame
{
    public class AgentSword : Agent
    {
        [SerializeField] private ParticleSystem earthShatter;
        [SerializeField] private Transform earthShatterTrm;
        
        private void PlayEarthShatter()
        {
            ParticleSystem p = Instantiate(earthShatter , earthShatterTrm.transform.position , Quaternion.LookRotation(earthShatterTrm.forward));
            p.Play();
        }
        
    }
}

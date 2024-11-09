using System.Linq;
using UnityEngine;

namespace CardGame
{
    public class AgentSword : Agent
    {
        [SerializeField] private ParticleSystem earthShatter;
        [SerializeField] private Transform earthShatterTrm;

        [SerializeField] private ParticleSystem[] slashEffect;
        
        private void PlayEarthShatter()
        {
            int rand = Random.Range(1, 4);
            float angleStep = rand == 2 ? 60 : (rand == 3 ? 30 : 0);

            for (int i = 0; i < rand; i++)
            {
                float angle = i * angleStep; 
                Quaternion rotation = Quaternion.LookRotation(earthShatterTrm.forward) * Quaternion.Euler(0, angle, 0); 
                ParticleSystem p = Instantiate(earthShatter, earthShatterTrm.transform.position, rotation);
                p.Play();
            }
           
        }

        private void PlaySlashEffect(int idx)
        {
            if (slashEffect[idx].isPlaying == false)
            {
                slashEffect[idx].Simulate(0);
                slashEffect[idx].Play();
            }
        }
    }
}

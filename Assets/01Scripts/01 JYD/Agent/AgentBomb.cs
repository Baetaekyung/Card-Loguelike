using UnityEngine;

namespace CardGame
{
    public class AgentBomb : Agent
    {
        public ParticleSystem bombParticle;
        
        public void Explosion()
        {
            ParticleSystem bomb = Instantiate(bombParticle,transform.position , transform.rotation);
            bomb.Simulate(0);
            bomb.Play();
            Destroy(gameObject);
        }
    }
}

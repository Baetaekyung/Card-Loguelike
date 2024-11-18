using UnityEngine;
using UnityEngine.Serialization;

public class AgentRange : Agent
{
    [SerializeField] private ParticleSystem _muzzleEffect;
    [FormerlySerializedAs("_bullet")] [SerializeField] private Projectile projectile;
    [SerializeField] private Transform firePos;
    
    
    public void PlayMuzzleEffect()
    {
        _muzzleEffect.Simulate(0);
        _muzzleEffect.Play();
    }

    public void Fire()
    {
        Projectile b = Instantiate(projectile,firePos.position , Quaternion.identity);
        b.Shot(-firePos.right , 20);
    }
    
}
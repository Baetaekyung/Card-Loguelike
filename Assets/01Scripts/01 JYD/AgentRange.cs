using UnityEngine;

public class AgentRange : Agent
{
    [SerializeField] private ParticleSystem _muzzleEffect;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform firePos;
    
    
    public void PlayMuzzleEffect()
    {
        _muzzleEffect.Simulate(0);
        _muzzleEffect.Play();
    }

    public void Fire()
    {
        Bullet b = Instantiate(_bullet,firePos.position , Quaternion.identity);
        b.Shot(-firePos.right , 20);
    }
    
}
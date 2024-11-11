using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsTarget;
    
    private Rigidbody _rigidbody;
    
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    public void Shot(Vector3 velocity,float power)
    {
        if (_rigidbody == null)
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        
        _rigidbody.linearVelocity = velocity * power;
        _rigidbody.rotation = Quaternion.LookRotation(velocity);
                
        StartCoroutine(IDestroy());
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if((whatIsTarget & (1 << other.gameObject.layer)) != 0)
            Destroy(gameObject);
    }

    IEnumerator IDestroy()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
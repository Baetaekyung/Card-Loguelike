using System.Collections;
using CardGame;
using UnityEngine;
using UnityEngine.InputSystem;

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
        if ((whatIsTarget & (1 << other.gameObject.layer)) != 0)
        {
            if (other.TryGetComponent(out IDamageable compo))
            {
                ActionData actionData = new ActionData();
                actionData.damageAmount = 10;
                
                compo.TakeDamage(actionData);
            }
            
            
            Destroy(gameObject);
        }
    }

    IEnumerator IDestroy()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
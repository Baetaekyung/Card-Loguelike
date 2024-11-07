using System;
using System.Collections;
using System.Data.Common;
using UnityEngine;

public class Bullet : MonoBehaviour
{
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
        Destroy(gameObject);
    }

    IEnumerator IDestroy()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
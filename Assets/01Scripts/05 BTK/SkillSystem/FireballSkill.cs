using System;
using CardGame.Players;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CardGame
{
    public class FireballSkill : MonoBehaviour
    {
        Collider[] c = new Collider[99];
        private Vector3 target;
       [SerializeField] private int _damage;
       [SerializeField] private float _speed;
        private void Start()
        {
            Destroy(gameObject, 15f);
             int cnt = Physics.OverlapSphereNonAlloc(transform.position, 50f, c);
             if (cnt > 0)
             {
                 for (int i = 0; i < cnt; i++)
                 {
                     if (c[i].name.Contains("Agent"))
                     {
                         target = c[i].transform.position;
                         break;
                     }
                 }
             }
             else
             {
                 target = new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100));
             }

             _rigid = GetComponent<Rigidbody>();
        }

        private Rigidbody _rigid;
        private void Update()
        {
            _rigid.AddForce((target - transform.position).normalized * _speed * Time.deltaTime);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(_damage);
            }

            Destroy(gameObject);
        }
    }
}

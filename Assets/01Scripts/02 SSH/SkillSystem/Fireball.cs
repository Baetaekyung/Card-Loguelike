using System;
using CardGame.Players;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CardGame
{
    public class Fireball : MonoBehaviour
    {
        Collider[] c = new Collider[99];
        private Vector3 target;
        [SerializeField] private float _speed;

        [SerializeField] private ActionData _data;
        private void Start()
        {
            target = Vector3.zero;
            Destroy(gameObject, 15f);
            int cnt = Physics.OverlapSphereNonAlloc(transform.position, 50f, c);
            if (cnt > 0)
            {
                for (int i = 0; i < cnt; i++)
                {
                    if (c[i].name.Contains("Agent"))
                    {
                        target = c[i].transform.position;
                        print("foundTarget");
                        break;
                    }
                }
            }
            if(target == Vector3.zero)
            {
                target = new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100));
            }

            _rigid = GetComponent<Rigidbody>();
        }

        private Rigidbody _rigid;
        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * _speed);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.transform.name.Equals("Fireball")) return;
            if (TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(_data);
                Destroy(gameObject);
            }

        }
    }
}
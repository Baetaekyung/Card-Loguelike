using System;
using System.Collections;
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
        private bool doMove;
        private bool clone = true;

        [SerializeField] private ActionData _data;
        private void Start()
        {
            doMove = false;
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
            if (clone)
            {
                for (int i = 0; i < 5; i++)
                {
                    Fireball fb = Instantiate(gameObject).GetComponent<Fireball>();
                    fb.ManualStart(0.5f * i);
                    fb.clone = false;
                }

                doMove = true;
            }
        }

        public void ManualStart(float waitTime)
        {
            StartCoroutine(StartMoving(waitTime));
        }

        private IEnumerator StartMoving(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            doMove = true;
        }

        private Rigidbody _rigid;
        private void Update()
        {
            if (!doMove) return;
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * _speed);
        }

        private void OnCollisionEnter(Collision other)
        {
            if(!doMove) return;
            if (TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(_data);
                Destroy(gameObject);
            }
            if (other.transform.name.Equals("Fireball")) return;//레이어 설정 해주고 다 바꿔주기
        }
    }
}
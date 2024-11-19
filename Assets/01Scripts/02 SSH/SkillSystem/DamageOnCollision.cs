using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

namespace CardGame
{
    public class DamageOnCollision : MonoBehaviour
    {
        public int _damage;
        public int _pushback;
        [SerializeField] private ActionData _data;
        public float removeTime = 1f;
        private float currentTime = 0;
        private void Update()
        {
            currentTime += Time.deltaTime;
            if(currentTime > removeTime) Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.TryGetComponent(out IDamageable damageable) && other.name.Contains("Agent"))//프로젝트 통합되고 실행되면 layer처리후 trigger에서 collision으로 바꾸기.
            {
                OnHit(damageable, other);
            }
        }

        public void OnHit(IDamageable agent, Collider other)
        {
            _data.damageAmount = _damage;
            _data.knockBackPower = _pushback;
            //_data.hitPoint = other.GetContact(0).point;
            //_data.hitNormal = other.GetContact(0).normal;

            agent.TakeDamage(_data);
            NavMeshAgent navMesh = other.GetComponent<NavMeshAgent>();
            float speed = navMesh.speed;
            navMesh.speed = 0;
            StartCoroutine(ReturnSpeed(speed, navMesh));
        }

        public IEnumerator ReturnSpeed(float beforeSpeed, NavMeshAgent navmesh)
        {
            yield return new WaitForSeconds(1f);
            navmesh.speed = beforeSpeed;
        }
    }
}

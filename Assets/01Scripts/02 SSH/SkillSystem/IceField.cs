
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CardGame
{
    public class IceField : DamageOnCollision
    {
        private void reset()
        {
            removeTime = 2f;
        }

        public override void OnHit(IDamageable agent, Collider other)
        {
            base.OnHit(agent, other);
            
            NavMeshAgent navMesh = other.GetComponent<NavMeshAgent>();
            float speed = navMesh.speed;
            navMesh.speed = 0;
            Debug.Log("speed to 0");
            StartCoroutine(ReturnSpeed(speed, navMesh));
        }
        public IEnumerator ReturnSpeed(float beforeSpeed, NavMeshAgent navmesh)
        {
            yield return new WaitForSeconds(1f);
            
            navmesh.speed = MathF.Max(beforeSpeed, navmesh.speed);
        }
        
    }
}

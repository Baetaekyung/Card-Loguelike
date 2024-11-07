using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour
{
    private const float MAX_ATK_DISTANCE = 50f;

    public Transform target;
    
    private NavMeshAgent _navMeshAgent;
    private Animator animator;
    
    private Vector3 _startPosition;
    private Vector3 _LastPosition;
    private Vector3 _nextPathPoint;
    
    private BehaviorGraphAgent _behaviorGraphAgent;
    
    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        _behaviorGraphAgent = GetComponent<BehaviorGraphAgent>();
        
        _LastPosition = transform.position;
    }

    private void Update()
    {
        FaceToTarget(GetNextPathPoint());
        _behaviorGraphAgent.GetVariable("ManualRotate", out BlackboardVariable<bool> isManualRotate);
        if (isManualRotate)
        {
            FaceToTarget(target.transform.position);
        }
        
        /*
        if (isManualMove)
        {
            transform.position = Vector3.MoveTowards(transform.position , _attackDestination , 
                15 * Time.deltaTime);
        }*/
    }
    
    private void LateUpdate()
    {
        Vector3 velocity = (transform.position - _LastPosition) / Time.deltaTime;
        _LastPosition = transform.position;
        
        velocity.y = 0;
        float speed = velocity.magnitude;
        
        if (speed > 0.5f)
        {
            animator.SetFloat("Speed", speed,0.1f , Time.deltaTime);
        }
        else
        {
            animator.SetFloat("Speed", 0,0.1f , Time.deltaTime);
        }
                
    }
    

    private void FaceToTarget(Vector3 _lookDir)
    {
        _behaviorGraphAgent.GetVariable("State" , out BlackboardVariable<State> enemyState);
        bool enterBattleMode = enemyState == State.Attack;// || enemyState == State.Chase;
        
        Vector3 targetPos = enterBattleMode ?
            target.position - transform.position : _lookDir - transform.position;
        targetPos.Normalize();
                
        if (targetPos != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(targetPos);
            Vector3 currentEulerAngle = transform.rotation.eulerAngles;

            float yRotation = Mathf.LerpAngle(currentEulerAngle.y, targetRot.eulerAngles.y, 5 * Time.deltaTime);
            transform.rotation = Quaternion.Euler(currentEulerAngle.x, yRotation, currentEulerAngle.z);
        }
    }

    
    private Vector3 GetNextPathPoint()
    {
        NavMeshPath path = _navMeshAgent.path;

        if(path.corners.Length < 2)
        {
            return _navMeshAgent.destination;
        }

        for(int i = 0; i < path.corners.Length; i++)
        {
            float distance = Vector3.Distance(_navMeshAgent.transform.position, path.corners[i]);

            if (distance < 1 && i < path.corners.Length - 1)
            {
                _nextPathPoint = path.corners[i + 1];
                return _nextPathPoint;
            }
        }

        return _nextPathPoint;
    }
    
    /*public void SetManualMove(bool _isManualMove)
    {
        isManualMove = _isManualMove;
    }
    public void SetManualRotate(bool _rotate)
    {
        isManualRotate = _rotate;
    }*/

    private void OnDrawGizmos()
    {
        if(Application.isPlaying == false)return;
        
        _behaviorGraphAgent.GetVariable("AttackRadius" , out BlackboardVariable<float> radius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position , radius);
        
        _behaviorGraphAgent.GetVariable("ChaseRadius" , out BlackboardVariable<float> chaseRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position , chaseRadius);
    }
}

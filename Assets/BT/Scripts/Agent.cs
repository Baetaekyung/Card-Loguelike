using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour
{
    private const float MAX_ATK_DISTANCE = 50f;

    public Transform target;
    
    private NavMeshAgent _navMeshAgent;
    private Animator animator;
    
    private Vector3 _attackDestination;
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

        _behaviorGraphAgent.GetVariable<bool>("ManualRotate", out BlackboardVariable<bool> isManualRotate);
        
        if (isManualRotate)
        {
            Vector3 moveDelta = transform.forward * MAX_ATK_DISTANCE;
            _attackDestination = _startPosition + moveDelta;
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
            animator.SetFloat("Speed", speed);
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }
    }
    

    public void FaceToTarget(Vector3 target)
    {
        Vector3 direction = target - transform.position;
    
        if (direction != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(direction);
            Vector3 currentEulerAngle = transform.rotation.eulerAngles;

            float yRotation = Mathf.LerpAngle(currentEulerAngle.y, targetRot.eulerAngles.y, 10 * Time.deltaTime);

            transform.rotation = Quaternion.Euler(currentEulerAngle.x, yRotation, currentEulerAngle.z);
        }
    }

    
    public Vector3 GetNextPathPoint()
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
    
    
}

using System.Collections;
using CardGame;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class AgentStat
{
    public float MoveSpeed;
    public float MaxHealth;
    public float Defense;
    public float DamageAmount;
    public float AttackSpeed;

    private BehaviorGraphAgent BehaviorGraphAgent;
    
    public AgentStat(BehaviorGraphAgent behaviorGraphAgent, float moveSpeed, float maxHealth,float defense, float damageAmount, float attackSpeed)
    {
        BehaviorGraphAgent = behaviorGraphAgent;
        MoveSpeed = moveSpeed;
        MaxHealth = maxHealth;
        DamageAmount = damageAmount;
        AttackSpeed = attackSpeed;
        Defense = defense;

        BehaviorGraphAgent.SetVariableValue("MoveSpeed",MoveSpeed);
        BehaviorGraphAgent.SetVariableValue("MaxHealth",MaxHealth);
        BehaviorGraphAgent.SetVariableValue("DamageAmount",DamageAmount);
        BehaviorGraphAgent.SetVariableValue("AttackSpeed",AttackSpeed);
        BehaviorGraphAgent.SetVariableValue("Defense",Defense);
    }
}

[RequireComponent(typeof(EnemyHealth))]
public class Agent : MonoBehaviour
{
    //public AgentStat Stat;
    
    private const float MAX_ATK_DISTANCE = 50f;

    public Transform target;
    
    private NavMeshAgent _navMeshAgent;
    private Rigidbody _rigidbody;
    private Animator animator;
    private BehaviorGraphAgent _behaviorGraphAgent;
    private EnemyHealth enemyHealth;
    
    public bool AnimationEnd => animationEnd;
    private Vector3 _LastPosition;
    private Vector3 _nextPathPoint;
    private Vector3 _startPosition;
    
    
    private bool canManualRotate;
    private bool animationEnd;
    
    [Header("Hit info")]
    [SerializeField] private ParticleSystem[] slashEffect;
        
    [Space]
    [Header("Knockback Info")]
    [SerializeField] private float _knockBackThreshold;
    [SerializeField] private float _maxKnockBackTime;
    
    private float _knockBackTime;
    private bool _isKnockBack;
   
    
    private void Awake()
    {
           
    }

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        _behaviorGraphAgent = GetComponent<BehaviorGraphAgent>();
        enemyHealth = GetComponent<EnemyHealth>();
        _rigidbody = GetComponent<Rigidbody>();
        
        _LastPosition = transform.position;
        
        if (target == null)
        {
            GameObject findTarget = GameObject.Find("Player");
            target = findTarget.transform;
            
            _behaviorGraphAgent.SetVariableValue("Target",findTarget.transform);
        }

        enemyHealth.OnDeadEvent += OnDead;
    }

    private void Update()
    {
        if(enemyHealth.IsAlive == false)return;
        
        Vector3 lookDir = canManualRotate? target.transform.position : GetNextPathPoint();
        FaceToTarget(lookDir);
        
    }
        
    private void LateUpdate()
    {
        if(_isKnockBack)return;
        
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
        Vector3 targetPos = _lookDir - transform.position;
        targetPos.Normalize();
        
        if (targetPos != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(targetPos);
            Vector3 currentEulerAngle = transform.rotation.eulerAngles;

            float yRotation = Mathf.LerpAngle(currentEulerAngle.y, targetRot.eulerAngles.y, 15 * Time.deltaTime);
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

    #region KnockbackInfo

    public void GetKnockBack(Vector3 force)
    {
        StartCoroutine(ApplyKnockBack(force));
    }
    
    private IEnumerator ApplyKnockBack(Vector3 force)
    {
        Vector3 originDestination = _navMeshAgent.destination;
                
        _navMeshAgent.enabled = false;
        _rigidbody.useGravity = true;
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(force, ForceMode.Impulse);
        _knockBackTime = Time.time;
        
        if (_isKnockBack)
        {
            yield break;
        }

        _isKnockBack = true;
        yield return new WaitForFixedUpdate(); 

        yield return new WaitUntil(CheckKnockBackEnd);
        DisableRigidbody();

        //_navMeshAgent.Warp(transform.position);
        _isKnockBack = false;

        if(enemyHealth.IsAlive)
        {
            _navMeshAgent.enabled = true;
            _navMeshAgent.SetDestination(originDestination);
        }
    }
    
    private bool CheckKnockBackEnd()
    {
        return _rigidbody.linearVelocity.magnitude < _knockBackThreshold || Time.time > _knockBackTime + _maxKnockBackTime;
    }
    
    private void DisableRigidbody()
    {
        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = true;
    }
    #endregion
    
    #region AnimationEvents
    
    private void SetManualStop()
    {
        _navMeshAgent.isStopped = true;
    }
    
    private void SetManualMove()
    {
        _navMeshAgent.isStopped = false;
    }
    
    private void SetManualRotate()
    {
        canManualRotate = true;
    }
    
    private void StopManualRotate()
    {
        canManualRotate = false;
    }

    public void SetAnimationEnd()
    {
        _behaviorGraphAgent.SetVariableValue("AnimationEnd",true);
        animationEnd = true;
        //print(animationEnd);
    }
    
    public void StopAnimationEnd()
    {
        _behaviorGraphAgent.SetVariableValue("AnimationEnd",false);
        animationEnd = false;
    }
    
    private void PlaySlashEffect(int idx)
    {
        if (slashEffect[idx].isPlaying == false)
        {
            slashEffect[idx].Simulate(0);
            slashEffect[idx].Play();
        }
    }
    
    #endregion

    private void OnDead()
    {
        _behaviorGraphAgent.SetVariableValue("State",State.Dead);
        _navMeshAgent.isStopped = true;
        _navMeshAgent.enabled = false;
        animator.SetTrigger("Dead");
        animator.SetBool("DeadBool",true);
        
        Destroy(gameObject,3f);
    }
    
    /*private void OnDrawGizmos()
    {
        if(Application.isPlaying == false)return;
        
        _behaviorGraphAgent.GetVariable("AttackRadius" , out BlackboardVariable<float> radius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position , radius);
        
        _behaviorGraphAgent.GetVariable("ChaseRadius" , out BlackboardVariable<float> chaseRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position , chaseRadius);
    }*/
}

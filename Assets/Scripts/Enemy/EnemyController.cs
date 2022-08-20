using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    enum EnemyState
    {
        Patrol = 0,
        Investigate = 1,
        Stunned = 2
    }
    
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _threshold = 1f;
    [SerializeField] private float _waitTime = 2f;
    [SerializeField] private float _stunnedTime = 3f;
    [SerializeField] private PatrolRoute _patrolRoute;
    [SerializeField] private FieldOfView _fieldOfView;
    [SerializeField] private EnemyState _state = EnemyState.Patrol;

    public UnityEvent<Transform> onPlayerFound;
    public UnityEvent onInvestigate;
    public UnityEvent onReturnToPatrol;
    public UnityEvent onStunned;

    private bool _moving = false;
    private Transform _currentPoint;
    private int _routeIndex = 0;
    private bool _forwardsAlongPath = true;
    private Vector3 _investigationPoint;
    private float _waitTimer = 0f;
    private bool _playerFound = false;
    private float _stunnedTimer = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        _currentPoint = _patrolRoute.route[_routeIndex];

        GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetFloat("Speed", _agent.velocity.magnitude);
        
        if (_fieldOfView.visibleObjects.Count > 0)
        {
            PlayerFound(_fieldOfView.visibleObjects[0].position);
        }
        
        if (_state == EnemyState.Patrol)
        {
            UpdatePatrol();
        }
        else if (_state == EnemyState.Investigate)
        {
            UpdateInvestigate();
        }
        else if (_state == EnemyState.Stunned)
        {
            _stunnedTimer += Time.deltaTime;
            
            if (_stunnedTimer >= _stunnedTime)
            {
                ReturnToPatrol();
                _animator.SetBool("Stunned", false);
            }
        }
    }

    public void SetStunned()
    {
        _animator.SetBool("Stunned", true);
        _stunnedTimer = 0f;
        _state = EnemyState.Stunned;
        _agent.SetDestination(transform.position);
        onStunned.Invoke();
    }

    public void InvestigatePoint(Vector3 investigationPoint)
    {
        SetInvestigationPoint(investigationPoint);
        
        onInvestigate.Invoke();
    }

    private void SetInvestigationPoint(Vector3 investigationPoint)
    {
        _state = EnemyState.Investigate;
        _investigationPoint = investigationPoint;
        _agent.SetDestination(_investigationPoint);
    }

    private void PlayerFound(Vector3 investigationPoint)
    {
        if (_playerFound)
        {
            return;
        }
        
        SetInvestigationPoint(investigationPoint);
        
        onPlayerFound.Invoke(_fieldOfView.creature.head);

        _playerFound = true;
    }

    private void UpdateInvestigate()
    {
        if (Vector3.Distance(transform.position, _investigationPoint) < _threshold)
        {
            _waitTimer += Time.deltaTime;
            if (_waitTimer > _waitTime)
            {
                ReturnToPatrol();
            }
        }
    }

    private void ReturnToPatrol()
    {
        _state = EnemyState.Patrol;
        _waitTimer = 0f;
        _moving = false;
        
        onReturnToPatrol.Invoke();
    }

    private void UpdatePatrol()
    {
        if (!_moving)
        {
            SetNextPatrolPoint();

            _agent.SetDestination(_currentPoint.position);
            _moving = true;
        }

        if (_moving && Vector3.Distance(transform.position, _currentPoint.position) < _threshold)
        {
            _moving = false;
        }
    }

    private void SetNextPatrolPoint()
    {
        if (_forwardsAlongPath)
        {
            _routeIndex++;
        }
        else
        {
            _routeIndex--;
        }
            
        if (_routeIndex == _patrolRoute.route.Count)
        {
            if (_patrolRoute.patrolType == PatrolRoute.PatrolType.Loop)
            {
                _routeIndex = 0;
            }
            else if (_patrolRoute.patrolType == PatrolRoute.PatrolType.PingPong)
            {
                _forwardsAlongPath = false;
                _routeIndex -= 2;
            }
        }

        if (_routeIndex == 0)
        {
            if (_patrolRoute.patrolType == PatrolRoute.PatrolType.PingPong)
            {
                _forwardsAlongPath = true;
            }
        }
            
        _currentPoint = _patrolRoute.route[_routeIndex];
    }
}

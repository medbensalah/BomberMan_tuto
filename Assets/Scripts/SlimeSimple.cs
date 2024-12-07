using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeSimple : EnemyBase
{
    [SerializeField] private List<Transform> _waypoints = new List<Transform>();
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _error = 0.1f;

    private bool _forward = true;
    private int _targetIndex;
    private Transform _target;

    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private static readonly int Death1 = Animator.StringToHash("Death");

    private bool _active = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
        _navMeshAgent.speed = _speed;
        _navMeshAgent.acceleration = 100;

        _targetIndex = 0;
        _target = _waypoints[_targetIndex];

        while (!_navMeshAgent.isOnNavMesh)
        {
            yield return new WaitForEndOfFrame();
        }
        
        _navMeshAgent.SetDestination(_target.position);
        _active = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_active) return;
        if ((transform.position - _target.position).sqrMagnitude > _error) return;

        if (_targetIndex >= _waypoints.Count - 1)
            _forward = true;
        else if (_targetIndex <= 0)
            _forward = false;

        if (_forward)
            _targetIndex--;
        else
            _targetIndex++;

        _target = _waypoints[_targetIndex];
        _navMeshAgent.SetDestination(_target.position);
    }

    protected override void Death()
    {
        _navMeshAgent.enabled = false;
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<BoxCollider2D>());
        _animator.SetTrigger(Death1);
    }

    public void End()
    {
        Destroy(gameObject);
    }
}

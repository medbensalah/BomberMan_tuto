using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimePatrol : EnemyBase
{
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _error = 0.1f;

    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private static readonly int Death1 = Animator.StringToHash("Death");

    private bool _active = false;
    [SerializeField] private float _range = 5;

    private Vector3 _target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
        _navMeshAgent.speed = _speed;
        _navMeshAgent.acceleration = 100;

        while (!_navMeshAgent.isOnNavMesh)
        {
            yield return new WaitForEndOfFrame();
        }

        _active = true;
        _target = new Vector3(3, -0.5f, 0);
        RandomPosition(transform.position, out _target);
        _navMeshAgent.SetDestination(_target);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_active) return;
        if (!_navMeshAgent.hasPath)
            SetDestination();
        if ((transform.position - _target).sqrMagnitude > _error) return;
        SetDestination();
    }

    protected override void Death()
    {
        _navMeshAgent.enabled = false;
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<BoxCollider2D>());
        _animator.SetTrigger(Death1);
    }

    private void SetDestination()
    {
        if (RandomPosition(transform.position, out _target))
        {
            _navMeshAgent.SetDestination(_target);
        }
    }

    private bool RandomPosition(Vector3 pos, out Vector3 target)
    {
        Vector3 randPoint = pos + Random.onUnitSphere * _range;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randPoint, out hit, _range, NavMesh.AllAreas))
        {
            target = hit.position;
            return true;
        }

        target = _target;
        return false;
    }

    public void End()
    {
        Destroy(gameObject);
    }
}
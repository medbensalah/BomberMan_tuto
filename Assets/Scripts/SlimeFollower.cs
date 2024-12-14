using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeFollower : EnemyBase
{
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _error = 0.1f;

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

        while (!_navMeshAgent.isOnNavMesh)
        {
            yield return new WaitForEndOfFrame();
        }
        
        _active = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_active) return;
        _navMeshAgent.SetDestination(PlayerMovementController.Instamce.transform.position);
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

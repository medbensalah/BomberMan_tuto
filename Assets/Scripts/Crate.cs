using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Crate : Breakable
{
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private BoxCollider2D _collider;

    [SerializeField] private GameObject _powerup;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    protected override void Explode()
    {
        InstantiatePowerup();
        Destroy(_rigidbody);
        Destroy(_collider);
        _animator.SetTrigger("Explode");
    }

    private void InstantiatePowerup()
    {
        float p = Random.Range(0.0f, 1.0f);
        if (p <= LevelGenerator.Instance.PowerupSpawnProbability)
        {
            Instantiate(_powerup, transform.position, Quaternion.identity);
        }
    }
    
    public void End()
    {
        Destroy(gameObject);
    }
}

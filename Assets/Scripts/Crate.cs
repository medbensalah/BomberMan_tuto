using System;
using UnityEngine;

public class Crate : Breakable
{
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private BoxCollider2D _collider;
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    protected override void Explode()
    {
        Destroy(_rigidbody);
        Destroy(_collider);
        _animator.SetTrigger("Explode");
    }

    public void End()
    {
        Destroy(gameObject);
    }
}

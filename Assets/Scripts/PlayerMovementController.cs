using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;

    private Vector2 _moveDir = Vector2.zero;
    private Rigidbody2D _rigidbody;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        _rigidbody.linearVelocity = _moveDir * _speed;
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Vector2 val = ctx.ReadValue<Vector2>();
            _moveDir.x = (val.x != 0 ? Mathf.Sign(val.x) : 0);
            _moveDir.y = (val.y != 0 ? Mathf.Sign(val.y) : 0);
        }
        if (ctx.canceled)
        {
            _moveDir = Vector2.zero;
        }
    }
}

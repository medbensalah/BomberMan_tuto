using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _snapError = 0.5f;

    private Vector2 _moveDir = Vector2.zero;
    private bool _isWalking = false;

    public bool IsWalking
    {
        get => _isWalking;
        set
        {
            if (value != _isWalking)
            {
                _isWalking = value;
                _animator.SetBool(Walking, value);
            }
        }
    }
        
        
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private static readonly int Walking = Animator.StringToHash("Walking");


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
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

            if (_moveDir.x != 0)
            {
                Vector3 scale = transform.localScale;
                transform.localScale = new Vector3(Mathf.Abs(scale.x) * Mathf.Sign(_moveDir.x), scale.y, scale.z);
            }
            Snap();
            IsWalking = true;
        }
        if (ctx.canceled)
        {
            _moveDir = Vector2.zero;
            Snap();
            IsWalking = false;
        }
    }


    private void Snap()
    {
        float x = _rigidbody.position.x;
        float y = _rigidbody.position.y;

        bool roundX = false;
        bool roundY = false;

        float xSnapTo = Mathf.Round(Mathf.Abs(x));
        if (Mathf.Abs(x) < xSnapTo + _snapError &&
            Mathf.Abs(x) > xSnapTo - _snapError &&
            _moveDir.x == 0)
            roundX = true;
        float ySnapTo = Mathf.Round(Mathf.Abs(y));
        if (Mathf.Abs(y) < ySnapTo + _snapError &&
            Mathf.Abs(y) > ySnapTo - _snapError &&
            _moveDir.y == 0)
            roundY = true;

        _rigidbody.position = new Vector2(roundX ? Mathf.Round(x) : x, roundY ? Mathf.Round(y) : y);
    }
}

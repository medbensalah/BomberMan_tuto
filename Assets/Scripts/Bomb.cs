using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float _timer = 2.0f;
    
    private BoxCollider2D _boxCollider;
    private float _countdown = 0.0f;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _boxCollider.enabled = false;
        _countdown = 0.0f;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_boxCollider.enabled)
        {
            if ((PlayerMovementController.Instamce.transform.position - transform.position).sqrMagnitude > 1.0f)
                _boxCollider.enabled = true;
        }

        _countdown += Time.deltaTime;
        if (_countdown > _timer)
        {
            Explode();
        }
    }

    private void Explode()
    {
        Destroy(gameObject);
    }
}

using System;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Explosion"))
        {
            Death();
        }
    }

    protected virtual void Death()
    {
        
    }
}

using System;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    protected virtual void Explode() {}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Explosion"))
        {
            Explode();
        }
    }
}

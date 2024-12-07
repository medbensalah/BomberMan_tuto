using System;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    private void Start()
    {
        Physics2D.SyncTransforms();
        NavMeshManager.Instance.UpdateNavMesh();
    }

    protected virtual void Explode() {}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Explosion"))
        {
            Explode();
        }
    }

    private void OnDestroy()
    {
        Physics2D.SyncTransforms();
        NavMeshManager.Instance.UpdateNavMesh();
    }
}

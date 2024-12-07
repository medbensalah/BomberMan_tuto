using System;
using NavMeshPlus.Components;
using UnityEngine;

public class NavMeshManager : MonoBehaviour
{
    public static NavMeshManager Instance;
    private NavMeshSurface _surface;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    void Start()
    {
        _surface = GetComponent<NavMeshSurface>();
        _surface.hideEditorLogs = true;
        _surface.BuildNavMeshAsync();
    }

    public void UpdateNavMesh()
    {
        _surface.UpdateNavMesh(_surface.navMeshData);
    }
}
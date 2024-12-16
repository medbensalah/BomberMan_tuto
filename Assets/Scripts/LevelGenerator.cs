using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator Instance;

    [Header("Crate")] [SerializeField] private GameObject _crate;
    public int MaxCrates = 10;
    public float PowerupSpawnProbability = 0.1f;

    [Header("Safe Area")] public Vector2 X_Y;

    private Vector2 _firstStone = new Vector2(-10, 2.5f);
    private Vector2 LevelSize = new Vector2(31, 11);
    private Vector2 MapOrigin = new Vector2(-11, 3.5f);

    private List<Vector2> _crates = new List<Vector2>();

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

        for (int i = 0; i < MaxCrates; i++)
        {
            Vector2 pos = new Vector2(
                (int)Random.Range(MapOrigin.x, MapOrigin.x + LevelSize.x),
                (int)Random.Range(MapOrigin.y, MapOrigin.y + LevelSize.y) + 0.5f
            );

            if (pos.x < X_Y.x && pos.y < X_Y.y)
            {
                i--;
                continue;
            }
            else
            {
                if (_crates.Contains(pos) || ((pos.x + 10) % 2 == 0 && (pos.y - 2.5f) % 2 == 0))
                {
                    // TODO
                }
            }
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
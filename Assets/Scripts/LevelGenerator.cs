using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator Instance;

    [Header("Crate")] [SerializeField] private GameObject _crateParent;
    [SerializeField] private GameObject _crate;
    public int MaxCrates = 25;
    public float PowerupSpawnProbability = 0.1f;

    [Header("Crate")] [SerializeField] private GameObject _enemyParent;
    [SerializeField] private GameObject _enemy;
    public int MaxEnemies = 6;
    
    [Header("Safe Area")] public Vector2 X_Y;

    private Vector2 _firstStone = new Vector2(-10, 2.5f);
    private Vector2 LevelSize = new Vector2(31, 11);
    private Vector2 MapOrigin = new Vector2(-11, 3.5f);

    private List<Vector2> _spawns = new List<Vector2>();

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

        # region Crates
        for (int i = 0; i < MaxCrates; i++)
        {
            Vector2 pos = new Vector2(
                (int)Random.Range((int)MapOrigin.x, (int)MapOrigin.x + (int)LevelSize.x),
                (int)Random.Range((int)MapOrigin.y - (int)LevelSize.y, (int)MapOrigin.y) + 1.5f
            );

            if (pos.x < X_Y.x && pos.y > X_Y.y)
            {
                i--;
                continue;
            }
            else
            {
                if (_spawns.Contains(pos) || ((pos.x + 10) % 2 == 0 && (pos.y - 2.5f) % 2 == 0))
                {
                    //TODO: change positions
                    i--;
                    continue;
                }
                else
                {
                    _spawns.Add(pos);
                    Instantiate(_crate, pos, Quaternion.identity, _crateParent.transform);
                }
            }
        }
        #endregion
        
        # region Enemy
        for (int i = 0; i < MaxEnemies; i++)
        {
            Vector2 pos = new Vector2(
                (int)Random.Range((int)MapOrigin.x, (int)MapOrigin.x + (int)LevelSize.x),
                (int)Random.Range((int)MapOrigin.y - (int)LevelSize.y, (int)MapOrigin.y) + 1.5f
            );

            if (pos.x < X_Y.x && pos.y > X_Y.y)
            {
                i--;
                continue;
            }
            else
            {
                if (_spawns.Contains(pos) || ((pos.x + 10) % 2 == 0 && (pos.y - 2.5f) % 2 == 0))
                {
                    //TODO: change positions
                    i--;
                    continue;
                }
                else
                {
                    _spawns.Add(pos);
                    Instantiate(_enemy, pos, Quaternion.identity, _enemyParent.transform);
                }
            }
        }
        #endregion
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
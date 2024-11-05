using System;
using UnityEngine;

public class CameraTracking : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Vector2 _xClamp;
    [SerializeField] private Vector2 _yClamp;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector2 pos = _player.position;
        float newX = Mathf.Clamp(pos.x, _xClamp.x, _xClamp.y);
        float newY = Mathf.Clamp(pos.y, _yClamp.x, _yClamp.y);
        transform.position = new Vector3(newX, newY, transform.position.z);
    }
}

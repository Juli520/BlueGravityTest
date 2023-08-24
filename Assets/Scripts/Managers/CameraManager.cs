using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Transform _player;

    private void Awake()
    {
        _player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        var t = transform;
        var playerPosition = _player.position;
        var newPos = new Vector3(playerPosition.x, playerPosition.y, t.position.z);
        t.position = newPos;
    }
}

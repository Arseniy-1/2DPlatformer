using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _stiffness = 0.05f;

    private void FixedUpdate()
    {
        if (_player == null)
            _player = FindObjectOfType<Player>();
        else
            transform.position = Vector3.Lerp(transform.position, new Vector3(_player.transform.position.x, _player.transform.position.y, transform.position.z), _stiffness);
    }
}

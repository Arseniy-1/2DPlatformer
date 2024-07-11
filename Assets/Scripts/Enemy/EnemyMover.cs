using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private EnemyDetector _enemyDetector;
    [SerializeField] private List<Transform> _waypoints;
    [SerializeField] private float _speed;

    private int _currentWaypointIndex = 0;
    private bool _haveDetectedEnemy = false;
    private Player _player;

    private bool _facingRight = false;

    private void OnEnable()
    {
        _enemyDetector.OnEnemyDetected += SelectTarget;
    }

    private void OnDisable()
    {
        _enemyDetector.OnEnemyDetected -= SelectTarget;
    }

    private void Update()
    {
        if (_haveDetectedEnemy)
            ChaseEnemy();
        else
            WaypointMove();

        CorrectFlipStatus();
    }

    private void Move(Transform targetPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, _speed * Time.deltaTime);
    }

    private void WaypointMove()
    {
        if (transform.position == _waypoints[_currentWaypointIndex].position)
            _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Count;

        Move(_waypoints[_currentWaypointIndex]);
    }

    private void ChaseEnemy()
    {
        if (_player == null)
        {
            _haveDetectedEnemy = false;
            return;
        }

        Move(_player.transform);
    }

    private void SelectTarget(Player player)
    {
        _player = player;
        _haveDetectedEnemy = true;
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    private void CorrectFlipStatus()
    {
        Transform currentTarget;

        if (_haveDetectedEnemy)
            currentTarget = _player.transform;
        else
            currentTarget = _waypoints[_currentWaypointIndex];

        if (!_facingRight && currentTarget.position.x > transform.position.x)
            Flip();
        else if (_facingRight && currentTarget.position.x < transform.position.x)
            Flip();
    }
}

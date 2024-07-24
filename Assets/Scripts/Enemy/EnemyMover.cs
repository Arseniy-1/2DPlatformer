using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyMover : Mover
{
    [SerializeField] private EnemyDetector _enemyDetector;
    [SerializeField] private List<Transform> _waypoints;
    [SerializeField] private float _speed;

    private int _currentWaypointIndex = 0;
    private bool _haveDetectedEnemy = false;
    private Player _player;

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
    }

    private void Move(Transform targetPosition)
    {
        HorizontalDirection = targetPosition.position.x - transform.position.x;
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
}

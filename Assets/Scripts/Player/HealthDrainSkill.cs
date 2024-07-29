using System;
using System.Collections;
using UnityEngine;

public class HealthDrainSkill : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _healthPerIteration;
    [SerializeField] private float _drainIterationDelay;
    [SerializeField] private Health _health;
    [SerializeField] private LayerMask _enemyLayerMask;

    private float _currentWorkTime = 0;
   
    private bool _isReady = false;
    private bool _isWorking = false;

    public event Action Activated;

    [field: SerializeField] public float CoolDown { get; private set; }
    [field: SerializeField] public float WorkTime { get; private set; }

    private void Start()
    {
        StartCoroutine(SkillRecharging());
    }

    public void Activate()
    {
        if (_isReady && _isWorking == false)
            StartCoroutine(HealthDraining());
    }

    private IEnumerator SkillRecharging()
    {
        yield return new WaitForSeconds(CoolDown);
        _isReady = true;
    }

    private IEnumerator HealthDraining()
    {
        _isWorking = true;
        Activated?.Invoke();

        WaitForSeconds delay = new WaitForSeconds(_drainIterationDelay);

        while (_currentWorkTime < WorkTime)
        {
            Drain();
            _currentWorkTime += _drainIterationDelay;
            yield return delay;
        }

        _isWorking = false;
        _isReady = false;
        _currentWorkTime = 0;

        StartCoroutine(SkillRecharging());
    }

    private void Drain()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _radius, _enemyLayerMask);

        foreach (Collider2D hit in hits)
        {
            if (hit.TryGetComponent(out Enemy enemy))
            {
                _health.Heal(enemy.TakeDamage(_healthPerIteration));
                break;
            }
        }
    }
}

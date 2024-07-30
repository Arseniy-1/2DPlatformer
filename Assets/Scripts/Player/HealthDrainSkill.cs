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

    [SerializeField, Range(4, 10)] private float _coolDown;
    [SerializeField, Range(0, 3)] private float _workTime;

    private float _currentWorkTime = 0;

    private bool _isReadyToActivate = false;

    private WaitForSeconds _coroutineDelay;

    public event Action<float> OnValueChanged;
    public event Action OnDiactivate;
    public event Action OnActivate;

    private void Awake()
    {
        _coroutineDelay = new WaitForSeconds(_drainIterationDelay);
    }

    private void Start()
    {
        StartCoroutine(SkillRecharging());
    }

    public void Activate()
    {
        if (_isReadyToActivate == true)
            StartCoroutine(HealthDraining());
    }

    private IEnumerator SkillRecharging()
    {
        _currentWorkTime = 0;

        while (_currentWorkTime < _coolDown)
        {
            _currentWorkTime += _drainIterationDelay;
            OnValueChanged?.Invoke(_currentWorkTime / _coolDown);
            yield return _coroutineDelay;
        }

        _isReadyToActivate = true;
    }

    private IEnumerator HealthDraining()
    {
        _isReadyToActivate = false;
        OnActivate?.Invoke();

        _currentWorkTime = _workTime;

        while (_currentWorkTime > 0)
        {
            Drain();
            _currentWorkTime -= _drainIterationDelay;
            OnValueChanged?.Invoke(_currentWorkTime / _workTime);
            yield return _coroutineDelay;
        }

        OnDiactivate?.Invoke();

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

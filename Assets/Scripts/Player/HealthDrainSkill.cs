using System.Collections;
using UnityEngine;

public class HealthDrainSkill : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _healthPerIteration;
    [SerializeField] private float _drainIterationDelay;
    [SerializeField] private float _workTime;
    [SerializeField] private float _coolDown;
    [SerializeField] private SkillView _skillView;
    [SerializeField] private Health _health;
    [SerializeField] private LayerMask _enemyLayerMask;

    private float _currentCoolDownTime = 0;
    private float _currentWorkTime = 0;
   
    private bool _isReady = false;
    private bool _isWorking = false;

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
        int oneSecondAmount = 1;
        _skillView.ShowSkillBar(_coolDown);

        while(_currentCoolDownTime < _coolDown)
        {
            _currentCoolDownTime += 1;
            yield return new WaitForSeconds(oneSecondAmount);
        }

        _isReady = true;
    }

    private IEnumerator HealthDraining()
    {
        _isWorking = true;

        _skillView.ShowRadius();
        _skillView.ReduceSkillBar(_workTime);

        WaitForSeconds delay = new WaitForSeconds(_drainIterationDelay);

        while (_currentWorkTime < _workTime)
        {
            Drain();
            _currentWorkTime += _drainIterationDelay;
            yield return delay;
        }

        _isWorking = false;
        _isReady = false;
        _currentWorkTime = 0;
        _currentCoolDownTime = 0;
        _skillView.HideRadius();

        StartCoroutine(SkillRecharging());
    }

    private void Drain()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _radius, _enemyLayerMask);

        foreach (Collider2D hit in hits)
        {
            if (hit.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(_healthPerIteration);
                _health.Heal(enemy.TakeDamage(_healthPerIteration));
            }
        }
    }
}

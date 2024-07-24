using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthDrainSkill : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _healthPerIteration;
    [SerializeField] private float _drainIterationDelay;
    [SerializeField] private float _workTime;
    [SerializeField] private float _coolDown;
    [SerializeField] private Slider _skillSlider;
    [SerializeField] private Health _health;

    private float _currentCoolDownTime = 0;
    private float _currentWorkTime = 0;
    private bool _isReady = false;
    private bool _isWorking = true;

    public void Activate()
    {
        if (_isReady)
        {
            StopAllCoroutines();
            StartCoroutine(HealthDraining());
        }
    }

    private void FixedUpdate()
    {
        if (_isWorking)
        {
            if (_currentCoolDownTime >= _coolDown)
                _isReady = true;

            if (_isReady == false)
                _currentCoolDownTime += Time.fixedDeltaTime;

            _skillSlider.value = _currentCoolDownTime / _coolDown;
        }
    }

    private IEnumerator HealthDraining()
    {
        _isWorking = false;
        WaitForSeconds delay = new WaitForSeconds(_drainIterationDelay);

        while (_currentWorkTime < _workTime)
        {
            Drain();
            _currentWorkTime += _drainIterationDelay;
            _skillSlider.value -= _drainIterationDelay / _workTime;
            yield return delay;
        }

        _isWorking = true;
        _isReady = false;
        _currentWorkTime = 0;
        _currentCoolDownTime = 0;
    }

    private void Drain()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _radius);

        foreach (Collider2D hit in hits)
        {
            if (hit.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(_healthPerIteration);
                _health.Heal(_healthPerIteration);
            }
        }
    }
}

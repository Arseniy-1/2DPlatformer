using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SmoothHealthBar : HealthBar
{
    [SerializeField] private float _smoothDecreaseDuration = 0.5f;

    private Coroutine _coroutine;

    protected override void ShowHealth()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(SmoothHealthChanging());
    }

    private IEnumerator SmoothHealthChanging()
    {
        float elapsedTime = 0f;
        float previousValue = _healthBar.value;

        while (elapsedTime < _smoothDecreaseDuration)
        {
            elapsedTime += Time.deltaTime;
            float normalizedPosition = elapsedTime / _smoothDecreaseDuration;
            float intermediateValue = Mathf.MoveTowards(previousValue, _health.CurrentHealthPoint / _health.MaxHealth, normalizedPosition);

            _healthBar.value = intermediateValue;

            yield return null;
        }
    }
}

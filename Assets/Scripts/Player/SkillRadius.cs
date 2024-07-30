using System.Collections;
using UnityEngine;

public class SkillRadius : SkillView
{
    [SerializeField] private SpriteRenderer _radiusSpriteRenderer;

    private float _minAlpha = 0;
    private float _maxAlpha = 0.5f;
    private float _currentAlpha = 0;
    private float _displayDelay = 0.05f;
    private float _alphaAmount = 0.1f;

    private WaitForSeconds _coroutineDisplayDelay;

    private void Awake()
    {
        _coroutineDisplayDelay = new WaitForSeconds(_displayDelay);
    }

    private void OnEnable()
    {
        HealthDrainSkill.OnActivate += Show;
        HealthDrainSkill.OnDiactivate += Hide;
    }

    private void OnDisable()
    {
        HealthDrainSkill.OnActivate -= Show;
        HealthDrainSkill.OnDiactivate -= Hide;
    }

    private void Show()
    {
        StartCoroutine(ChangingRadiusAlpha(_alphaAmount, _maxAlpha));
    }

    private void Hide()
    {
        StartCoroutine(ChangingRadiusAlpha(-_alphaAmount, _minAlpha));
    }

    private IEnumerator ChangingRadiusAlpha(float alphaAmount, float targetAmount)
    {
        while (FloatComparator(_currentAlpha, targetAmount))
        {
            _currentAlpha += alphaAmount;
            _radiusSpriteRenderer.color = new Color(_radiusSpriteRenderer.color.r, _radiusSpriteRenderer.color.g, _radiusSpriteRenderer.color.b, _currentAlpha);
            yield return _coroutineDisplayDelay;
        }
    }

    private bool FloatComparator(float firstNumber, float secondNumber)
    {
        int multiplier = 10;

        return Mathf.Round(firstNumber * multiplier) != secondNumber * multiplier;
    }
}
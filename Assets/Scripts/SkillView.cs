using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SkillView : MonoBehaviour
{
    [SerializeField] private Slider _skillSlider;
    [SerializeField] private SpriteRenderer _radiusSpriteRenderer;
    [SerializeField] private HealthDrainSkill _healthDrainSkill;

    private float _minAlpha = 0;
    private float _maxAlpha = 0.5f;
    private float _currentAlpha = 0;
    private float _displayDelay = 0.05f;

    private WaitForSeconds _coroutineDisplayDelay;

    private void OnEnable()
    {
        _healthDrainSkill.Activated += ShowActivatedBar;
    }

    private void OnDisable()
    {
        _healthDrainSkill.Activated -= ShowActivatedBar;
    }

    private void Awake()
    {
        _coroutineDisplayDelay = new WaitForSeconds(_displayDelay);
    }

    private void Start()
    {
        StartCoroutine(SmoothShowingSkillBar());
    }

    public void ShowActivatedBar()
    {
        StartCoroutine(SmoothShowRadius());
        StartCoroutine(SmoothHidingSkillBar());
    }

    private IEnumerator SmoothShowRadius()
    {
        float alphaAmount = 0.1f;

        while (_currentAlpha <= _maxAlpha)
        {
            _currentAlpha += alphaAmount;
            _radiusSpriteRenderer.color = new Color(_radiusSpriteRenderer.color.r, _radiusSpriteRenderer.color.g, _radiusSpriteRenderer.color.b, _currentAlpha);
            yield return _coroutineDisplayDelay;
        }

        yield return new WaitForSeconds(_healthDrainSkill.WorkTime);

        while (_currentAlpha >= _minAlpha)
        {
            _currentAlpha -= alphaAmount;
            _radiusSpriteRenderer.color = new Color(_radiusSpriteRenderer.color.r, _radiusSpriteRenderer.color.g, _radiusSpriteRenderer.color.b, _currentAlpha);
            yield return _coroutineDisplayDelay;
        }
    }

    private IEnumerator SmoothHidingSkillBar()
    {
        float sliderMaxValue = 1;
        float sliderMinValue = 0.01f;

        while (_skillSlider.value > sliderMinValue)
        {
            _skillSlider.value -= sliderMaxValue / (_healthDrainSkill.WorkTime / _displayDelay);
            yield return _coroutineDisplayDelay;
        }

        StartCoroutine(SmoothShowingSkillBar());
    }

    private IEnumerator SmoothShowingSkillBar()
    {
        float sliderMaxValue = 1;

        while (_skillSlider.value < sliderMaxValue)
        {
            _skillSlider.value += sliderMaxValue / (_healthDrainSkill.CoolDown / _displayDelay);
            yield return _coroutineDisplayDelay;
        }
    }
}
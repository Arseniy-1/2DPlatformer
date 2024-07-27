using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SkillView : MonoBehaviour
{
    [SerializeField] private Slider _skillSlider;
    [SerializeField] private RadiusView _radiusView;
    [SerializeField] private SpriteRenderer _radiusSpriteRenderer;

    private float _minAlpha = 0;
    private float _maxAlpha = 0.5f;
    private float _currentAlpha = 0;
    private float _displayDelay = 0.05f;

    public void SetSliderValue(float amount)
    {
        _skillSlider.value = amount;
    }

    public void ShowRadius()
    {
        StartCoroutine(SmoothShowRadius(_maxAlpha));
    }

    public void HideRadius()
    {
        StartCoroutine(SmoothHideRadius(_minAlpha));
    }

    public void ReduceSkillBar(float workTime)
    {
        StartCoroutine(SmoothReducingSkillBar(workTime));
    }

    public void ShowSkillBar(float workTime)
    {
        StartCoroutine(SmoothShowSkillBar(workTime));
    }

    private IEnumerator SmoothShowRadius(float targetAlpha)
    {
        float alphaAmount = 0.1f;

        while (_currentAlpha <= targetAlpha)
        {
            _currentAlpha += alphaAmount;
            _radiusSpriteRenderer.color = new Color(_radiusSpriteRenderer.color.r, _radiusSpriteRenderer.color.g, _radiusSpriteRenderer.color.b, _currentAlpha);
            yield return new WaitForSeconds(_displayDelay);
        }
    }

    private IEnumerator SmoothHideRadius(float targetAlpha)
    {
        float alphaAmount = -0.1f;

        while (_currentAlpha >= targetAlpha)
        {
            _currentAlpha += alphaAmount;
            _radiusSpriteRenderer.color = new Color(_radiusSpriteRenderer.color.r, _radiusSpriteRenderer.color.g, _radiusSpriteRenderer.color.b, _currentAlpha);
            yield return new WaitForSeconds(_displayDelay);
        }
    }

    private IEnumerator SmoothReducingSkillBar(float time)
    {
        float sliderMaxValue = 1;
        float sliderMinValue = 0.01f;

        while (_skillSlider.value > sliderMinValue)
        {
            _skillSlider.value -= sliderMaxValue / (time / _displayDelay);
            yield return new WaitForSeconds(_displayDelay);
        }
    }

    private IEnumerator SmoothShowSkillBar(float time)
    {
        float sliderMaxValue = 1;

        while (_skillSlider.value < sliderMaxValue)
        {
            _skillSlider.value += sliderMaxValue / (time / _displayDelay);
            yield return new WaitForSeconds(_displayDelay);
        }
    }
}

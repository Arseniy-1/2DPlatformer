using UnityEngine;
using UnityEngine.UI;

public class SkillBarView : SkillView
{
    [SerializeField] private Slider _skillSlider;

    private void OnEnable()
    {
        HealthDrainSkill.OnValueChanged += SetValue;
    }

    private void OnDisable()
    {
        HealthDrainSkill.OnValueChanged -= SetValue;
    }
    private void SetValue(float amount)
    {
        _skillSlider.value = amount;
    }
}
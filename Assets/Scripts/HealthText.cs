using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthText : HealthView
{
    [SerializeField] private TextMeshProUGUI _healthView;

    protected override void ShowHealth()
    {
        float amount = _health.CurrentHealthPoint;
        _healthView.text = amount.ToString();
    }
}

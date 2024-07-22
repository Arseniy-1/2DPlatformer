using UnityEngine;
using UnityEngine.UI;

public class HealthBar : HealthView
{
    [SerializeField] protected Slider _healthBar;

    protected override void ShowHealth()
    {
        _healthBar.value = _health.CurrentHealthPoint / _health.MaxHealth;
    }

}

using TMPro;
using UnityEngine;

public class HealthView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthView;
    [SerializeField] private Health _health;

    private void OnEnable()
    {
        _health.HealthChanged += ShowHealth;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= ShowHealth;
    }

    private void ShowHealth()
    {
        int amount = _health.CurrentHealthPoint;
        _healthView.text = amount.ToString();
    }
}

using UnityEngine;

public abstract class HealthView : MonoBehaviour
{
    [SerializeField] protected Health _health;

    private void OnEnable()
    {
        _health.HealthChanged += ShowHealth;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= ShowHealth;
    }

    protected abstract void ShowHealth();
}
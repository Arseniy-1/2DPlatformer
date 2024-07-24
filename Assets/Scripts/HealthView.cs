using UnityEngine;

public abstract class HealthView : MonoBehaviour
{
    [SerializeField] private Health _health;

    private RectTransform _healthTransform;

    private void Start()
    {
        _healthTransform = _health.GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        _health.HealthChanged += ShowHealth;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= ShowHealth;
    }

    protected abstract void ShowHealth(float currentHealth, float maxHealth);

    public void FlipHealth()
    {
        _healthTransform.localScale = new Vector3(-_healthTransform.localScale.x, _healthTransform.localScale.y, _healthTransform.localScale.z);
    }
}
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _currentHealthPoint;

    public event Action<float, float> HealthChanged;
    public event Action Died;


    private void Awake()
    {
        _currentHealthPoint = _maxHealth;
    }

    public void Heal(int amount)
    {
        if (amount <= 0)
            return;

        _currentHealthPoint = Mathf.Clamp(_currentHealthPoint + amount, 0, _maxHealth);

        HealthChanged?.Invoke(_currentHealthPoint, _maxHealth);
    }

    public void TakeDamage(int amount)
    {
        if (amount <= 0)
            return;

        _currentHealthPoint = Mathf.Clamp(_currentHealthPoint - amount, 0, _maxHealth);
        
        if (_currentHealthPoint == 0)
            Died.Invoke();

        HealthChanged?.Invoke(_currentHealthPoint, _maxHealth);
    }
}
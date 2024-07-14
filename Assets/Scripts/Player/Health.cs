using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    public event Action HealthChanged;

    [SerializeField] private int _maxHealth;

    public int CurrentHealthPoint { get; private set; }

    private void Awake()
    {
        CurrentHealthPoint = _maxHealth;
    }

    public void Heal(int amount)
    {
        if (amount <= 0)
            return;

        if (CurrentHealthPoint + amount <= _maxHealth)
        {
            CurrentHealthPoint = _maxHealth;
            HealthChanged?.Invoke();
            return;
        }

        CurrentHealthPoint += amount;
        HealthChanged?.Invoke();
    }

    public void TakeDamage(int amount)
    {
        if (amount <= 0)
            return;

        CurrentHealthPoint -= amount;

        if (CurrentHealthPoint <= 0)
        {
            Destroy(gameObject);
            CurrentHealthPoint = 0;
        }

        HealthChanged?.Invoke();
    }
}

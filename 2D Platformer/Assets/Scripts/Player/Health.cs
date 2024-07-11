using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    public event Action HealthChanged;

    public int CurrentHealthPoint { get; private set; } = 100;
    private int _maxHealth;

    private void Awake()
    {
        _maxHealth = CurrentHealthPoint;
    }

    public void Heal(int amount)
    {
        if (amount <= 0)
            return;
        
        if(CurrentHealthPoint + amount > _maxHealth)
        {
            CurrentHealthPoint = _maxHealth;
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
            Destroy(gameObject);

        HealthChanged?.Invoke();
    }
}

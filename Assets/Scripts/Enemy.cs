using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] protected int Damage;
    [SerializeField] protected HealthView HealthView;
    [SerializeField] protected Health Health;

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Player player))
            if (player.TryGetComponent(out Health health))
                health.TakeDamage(Damage);
    }

    public void TakeDamage(int amount)
    {
        Health.TakeDamage(amount);
    }
}
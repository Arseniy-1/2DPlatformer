using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] protected int Damage;
    [SerializeField] protected HealthView HealthView;
    [SerializeField] protected Health Health;

    private void OnEnable()
    {
        Health.Died += MakeDeath;
    }

    private void OnDisable()
    {
        Health.Died -= MakeDeath;
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Player player))
            if (player.TryGetComponent(out Health health))
                health.TakeDamage(Damage);
    }

    public void TakeDamage(float amount)
    {
        Health.TakeDamage(amount);
    }

    private void MakeDeath()
    {
        Destroy(gameObject);
    }
}
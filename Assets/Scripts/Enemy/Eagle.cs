using UnityEngine;

public class Eagle : MonoBehaviour, IDamageable
{
    [SerializeField] private int _health;
    [SerializeField] private int _damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Player player))
            if (player.TryGetComponent(out Health health))
                health.TakeDamage(_damage);
    }

    public void TakeDamage(int amount)
    {
        if (amount <= 0)
            return;

        _health -= amount;

        if (_health <= 0)
            Destroy(gameObject);
    }

}

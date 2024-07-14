using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private int _damage;

    private float _attackRange = 0.5f;

    public void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange);

        foreach(Collider2D hit in hits)
        {
            if(hit.TryGetComponent(out IDamageable damageable)) 
                damageable.TakeDamage(_damage);
        }
    }
}

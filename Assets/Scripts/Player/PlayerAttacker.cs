using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private int _damage;

    public void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(_attackPoint.position, 0.5f);

        foreach(Collider2D hit in hits)
        {
            if(hit.TryGetComponent(out IDamageable damageable)) 
                damageable.TakeDamage(_damage);
        }
    }
}

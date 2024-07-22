using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private int _damage;
    [SerializeField] private AttackAnimation _attackAnimation;

    private float _attackRange = 0.5f;

    public void Attack()
    {
        _attackAnimation.ShowAnimation();

        Collider2D[] hits = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange);

        foreach(Collider2D hit in hits)
        {
            if(hit.TryGetComponent(out IDamageable damageable)) 
                damageable.TakeDamage(_damage);
        }
    }
}

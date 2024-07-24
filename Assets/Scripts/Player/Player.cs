using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(PlayerMover))]

public class Player : MonoBehaviour
{
    [SerializeField] private CollisionHandler _collisionHandler;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private Health _heath;
    [SerializeField] private PlayerAttacker _playerAttacker;
    [SerializeField] private HealthDrainSkill _playerDrainSkill;

    private KeyCode _attackButton = KeyCode.E;
    private KeyCode _skillButton = KeyCode.F;

    private void OnEnable()
    {
        _heath.Died += MakeDeath;
        _collisionHandler.Triggered += OnTriggered;
    }

    private void OnDisable()
    {
        _heath.Died -= MakeDeath;
        _collisionHandler.Triggered -= OnTriggered;
    }

    private void Update()
    {
        if (Input.GetKeyDown(_attackButton))
        {
            _playerAttacker.Attack();
        }
        else if (Input.GetKeyDown(_skillButton))
        {
            _playerDrainSkill.Activate();
        }
    }

    private void OnTriggered(Collider2D collision)
    {
        if (collision.TryGetComponent(out IPickable pickable))
        {
            PickUp((dynamic)pickable);
            pickable.PickUp();
        }
        else if (collision.TryGetComponent(out ExitZone exitZone))
        {
            Destroy(gameObject);
        }
    }

    private void PickUp(Coin coin)
    {
        _wallet.AddCoin();
    }

    private void PickUp(Heal heal)
    {
        _heath.Heal(heal.HealAmount);
    }

    private void MakeDeath()
    {
        Destroy(gameObject);
    }
}

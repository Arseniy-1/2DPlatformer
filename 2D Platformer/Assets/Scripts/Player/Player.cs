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

    private KeyCode _attackButton = KeyCode.E;

    private void OnEnable()
    {
        _collisionHandler.Triggered += OnTriggered;
    }

    private void OnDisable()
    {
        _collisionHandler.Triggered -= OnTriggered;
    }

    private void Update()
    {
        if (Input.GetKeyDown(_attackButton))
        {
            _playerAttacker.Attack();
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
            Debug.Log("Игра окончена");
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
}

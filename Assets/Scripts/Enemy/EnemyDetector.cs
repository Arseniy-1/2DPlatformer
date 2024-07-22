using System;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    private Player _detectedPlayer;

    public event Action<Player> OnEnemyDetected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        { 
            if(player == _detectedPlayer)
                return;

            _detectedPlayer = player;
            OnEnemyDetected?.Invoke(player);
        }
    }
}

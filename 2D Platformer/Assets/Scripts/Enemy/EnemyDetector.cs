using System;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    public event Action<Player> OnEnemyDetected;
    private Player _detectedPlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        { 
            if(player == _detectedPlayer)
                return;

            OnEnemyDetected?.Invoke(player);
            _detectedPlayer = player;
        }
    }
}

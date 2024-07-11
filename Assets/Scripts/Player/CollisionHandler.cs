using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public event Action<Collider2D> Triggered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Triggered?.Invoke(collision);
    }
}

using UnityEngine;

public class Heal : MonoBehaviour, IPickable
{
    [field: SerializeField] public int HealAmount { get; private set; } = 100;

    public void PickUp()
    {
        Destroy(gameObject);
    }
}

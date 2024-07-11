using UnityEngine;
using System;

public class Wallet : MonoBehaviour
{
    public event Action MoneyChanged;

    public int Money { get; private set; } = 0;

    public void AddCoin()
    {
        Money++;
        MoneyChanged?.Invoke();
    }
}

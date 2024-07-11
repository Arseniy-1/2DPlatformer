using TMPro;
using UnityEngine;

public class WalleView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyView;
    [SerializeField] private Wallet _wallet;

    private void OnEnable()
    {
        _wallet.MoneyChanged += ShowMoney;
    }

    private void OnDisable()
    {
        _wallet.MoneyChanged -= ShowMoney;
    }

    private void ShowMoney()
    {
        int amount = _wallet.Money;
        _moneyView.text = amount.ToString();
    }
}

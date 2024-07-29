using TMPro;
using UnityEngine;

public class WalletDisplay : MonoBehaviour
{
    private const string CHANGE_MONEY = "ChangeMoney";

    [SerializeField] private TMP_Text _label;
    [SerializeField] private Animator _animator;

    public void OnEnable()
    {
        Wallet.CoinChanged += OnCoinChanged;
        _label.text = Wallet.CoinValue.ToString();
    }

    private void OnDisable()
    {
        Wallet.CoinChanged -= OnCoinChanged;
    }

    private void OnCoinChanged(int value)
    {
        _label.text = value.ToString();
        _animator.Play(CHANGE_MONEY);
    }
}
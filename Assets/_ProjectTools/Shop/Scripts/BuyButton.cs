using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _coinPanel;
    [SerializeField] private GameObject _videoAdPanel;
    [SerializeField] private GameObject _yanPanel;
    [SerializeField] private GameObject _noMoneyDisplay;
    [SerializeField] private TMP_Text _coinPrice;
    [SerializeField] private TMP_Text _yanPrice;

    public event Action Click;

    private void OnEnable() => _button.onClick.AddListener(OnClick);

    private void OnDisable() => _button.onClick.RemoveListener(OnClick);

    public void Initialize(ItemInfo itemInfo)
    {
        HideAllButtons();

        switch (itemInfo.CashType)
        {
            case CashType.Coin:
                if (Wallet.CoinValue < itemInfo.Price)
                {
                    _button.interactable = false;
                    _noMoneyDisplay.SetActive(true);
                }

                _coinPanel.SetActive(true);
                _coinPrice.text = itemInfo.Price.ToString();
                break;
            case CashType.VideoAd:
                _videoAdPanel.SetActive(true);
                break;
            case CashType.Yan:
                _yanPanel.SetActive(true);
                _yanPrice.text = itemInfo.Price.ToString();
                break;
        }
    }

    private void HideAllButtons()
    {
        _noMoneyDisplay.SetActive(false);
        _coinPanel.SetActive(false);
        _videoAdPanel.SetActive(false);
        _yanPanel.SetActive(false);
        _button.interactable = true;
    }

    private void OnClick() => Click?.Invoke();
}
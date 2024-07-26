using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopItemView : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _background;
    [SerializeField] private Image _contentImage;
    [SerializeField] private TMP_Text _coinPrice;
    [SerializeField] private TMP_Text _yanPrice;

    [SerializeField] private Sprite _lockBackground;
    [SerializeField] private Sprite _unLockBackground;
    [SerializeField] private Sprite _higlighBackground;
    [SerializeField] private GameObject _buyedPanel;
    [SerializeField] private GameObject _selectedPanel;
    [SerializeField] private GameObject _coinPanel;
    [SerializeField] private GameObject _videoAdPanel;
    [SerializeField] private GameObject _yanPanel;

    private GameObject _currentPricePanel;
    private OpenItemChecker _openItemChecker;

    public bool IsLock { get; private set; }
    public ItemInfo ItemInfo { get; private set; }

    public event Action<ShopItemView> Click;

    public void Initialize(ItemInfo itemInfo, OpenItemChecker openItemChecker)
    {
        ItemInfo = itemInfo;
        _contentImage.sprite = itemInfo.Icon;
        _openItemChecker = openItemChecker;

        switch (itemInfo.CashType)
        {
            case CashType.Coin:
                _currentPricePanel = _coinPanel;
                _coinPrice.text = itemInfo.Price.ToString();
                break;
            case CashType.VideoAd:
                _currentPricePanel = _videoAdPanel;
                break;
            case CashType.Yan:
                _currentPricePanel = _yanPanel;
                _yanPrice.text = itemInfo.Price.ToString();
                break;
            case CashType.Win:
                break;
            default:
                _currentPricePanel = _coinPanel;
                break;
        }
    }

    public void Lock()
    {
        _background.sprite = _lockBackground;
        _currentPricePanel.SetActive(true);
    }

    public void UnLock()
    {
        _background.sprite = _unLockBackground;
        _currentPricePanel.SetActive(false);
    }

    public void Select()
    {
        _background.sprite = _higlighBackground;
        _currentPricePanel.SetActive(false);
        _buyedPanel.SetActive(false);
        _selectedPanel.SetActive(true);
    }

    public void UnSelect()
    {
        _selectedPanel.SetActive(false);
        _openItemChecker.Visit(ItemInfo);

        if (_openItemChecker.IsOpened)
            _buyedPanel.SetActive(true);
    }

    public void Highlight() => _background.sprite = _higlighBackground;
    public void UnHighlight()
    {
        _openItemChecker.Visit(ItemInfo);

        if (_openItemChecker.IsOpened)
            _background.sprite = _unLockBackground;
        else
            _background.sprite = _lockBackground;
    }

    public void OnPointerClick(PointerEventData eventData) => Click?.Invoke(this);
}
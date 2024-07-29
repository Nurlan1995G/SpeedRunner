using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;
using YG.Utils.Pay;

public class Shop : MonoBehaviour
{
    [SerializeField] private ShopContent _shopContent;
    [SerializeField] private ShopPanel _shopPanel;
    [SerializeField] private SkinPlacement _skinPlacement;

    private ItemSelector _itemSelector;
    private ItemUnlocker _itemUnlocker;
    private OpenItemChecker _openItemChecker;
    private SelectedItemChecker _selectedItemChecker;

    private ShopItemView _shopItemView;

    public bool IsInitialized { get; private set; }

    #region SHOP_PANEL_BUTTONS
    [SerializeField] private BuyButton _buyButton;
    [SerializeField] private Button _selectionButton;
    [SerializeField] private TMP_Text _selectedText;
    #endregion

    #region CATEGORY_BUTTONS
    [SerializeField] private ShopCategoryButton _skinButton;
    [SerializeField] private ShopCategoryButton _objectButton;
    [SerializeField] private ShopCategoryButton _trailButton;
    [SerializeField] private ShopCategoryButton _animalButton;
    [SerializeField] private ShopCategoryButton _softButton;
    #endregion

    private void OnEnable()
    {
        if (_skinButton != null)
            _skinButton.Click += OnSkinButtonClick;

        if (_objectButton != null)
            _objectButton.Click += OnObjectButtonClick;

        if (_trailButton != null)
            _trailButton.Click += OnTrailButtonClick;

        if (_animalButton != null)
            _animalButton.Click += OnAnimalButtonClick;

        if (_skinButton != null)
            _softButton.Click += OnSoftButtonClick;

        _buyButton.Click += OnBuyButtonClick;
        _shopPanel.ItemViewClicked += OnItemViewClicked;
        _selectionButton.onClick.AddListener(OnSelecltedButtonClick);
        OnSkinButtonClick();
        YandexGame.PurchaseSuccessEvent += SuccessPurchased;
        YandexGame.PurchaseFailedEvent += FailedPurchased;
    }

    private void OnDisable()
    {
        _skinPlacement.Clear();

        if (_skinButton != null)
            _skinButton.Click -= OnSkinButtonClick;

        if (_objectButton != null)
            _objectButton.Click -= OnObjectButtonClick;

        if (_trailButton != null)
            _trailButton.Click -= OnTrailButtonClick;

        if (_animalButton != null)
            _animalButton.Click -= OnAnimalButtonClick;

        if (_skinButton != null)
            _softButton.Click -= OnSoftButtonClick;

        _shopPanel.ItemViewClicked -= OnItemViewClicked;

        _buyButton.Click -= OnBuyButtonClick;
        _selectionButton.onClick.RemoveListener(OnSelecltedButtonClick);
        YandexGame.PurchaseSuccessEvent -= SuccessPurchased;
        YandexGame.PurchaseFailedEvent += FailedPurchased;
    }

    private void FailedPurchased(string id) => YandexSDK.Instance.MuteAudio(YandexSDK.Instance.Data.IsMute);

    private void SuccessPurchased(string id) => YandexSDK.Instance.MuteAudio(YandexSDK.Instance.Data.IsMute);

    public void Initialize(OpenItemChecker openItemChecker, SelectedItemChecker selectedItemChecker, ItemSelector itemSelector, ItemUnlocker itemUnlocker)
    {
        _itemSelector = itemSelector;
        _openItemChecker = openItemChecker;
        _selectedItemChecker = selectedItemChecker;
        _itemUnlocker = itemUnlocker;

        YandexGame.PurchaseSuccessEvent += OnPurchaseSuccessEvent;

        _shopPanel.Initialize(_openItemChecker, _selectedItemChecker);
        _shopPanel.ItemViewClicked += OnItemViewClicked;
        IsInitialized = true;
    }

    private void OnPurchaseSuccessEvent(string id)
    {
        Billing.ConsumeProduct(id, () =>
        {
            int valueId = int.Parse(id);

            SetPurchasedProducts(_shopContent.SkinItemInfos.FirstOrDefault(itemInfo => itemInfo.YanId == valueId));
            SetPurchasedProducts(_shopContent.ObjectItemInfos.FirstOrDefault(itemInfo => itemInfo.YanId == valueId));
            SetPurchasedProducts(_shopContent.TrailItemInfos.FirstOrDefault(itemInfo => itemInfo.YanId == valueId));
            SetPurchasedProducts(_shopContent.AnimalItemInfos.FirstOrDefault(itemInfo => itemInfo.YanId == valueId));
            SetPurchasedProducts(_shopContent.SoftItemInfos.FirstOrDefault(itemInfo => itemInfo.YanId == valueId));
        });

        YandexSDK.Instance.Save();
        _shopPanel.Initialize(_openItemChecker, _selectedItemChecker);
        _shopPanel.ItemViewClicked += OnItemViewClicked;
        IsInitialized = true;
    }

    private void SetPurchasedProducts(ItemInfo itemInfo)
    {
        if (itemInfo != null)
        {
            if (itemInfo.ItemType == ItemType.Soft)
            {
                var softItemInfo = itemInfo as SoftItemInfo;
                Wallet.Add(softItemInfo.Reward);
            }
            else
            {
                _itemUnlocker.Visit(itemInfo);
                _shopItemView.UnLock();
            }
        }
    }

    private void OnItemViewClicked(ShopItemView shopItemView)
    {
        _shopItemView = shopItemView;
        _skinPlacement.InstantiateModel(shopItemView.ItemInfo.ShopPreview, shopItemView.ItemInfo.IsDynamic);

        _openItemChecker.Visit(_shopItemView.ItemInfo);

        if (_openItemChecker.IsOpened)
        {
            _selectedItemChecker.Visit(_shopItemView.ItemInfo);

            if (_selectedItemChecker.IsSelected)
            {
                ShowSelectedButton();
                return;
            }

            ShowSelectionButton();
        }
        else
        {
            ShowBuyButton(shopItemView.ItemInfo);
        }
    }

    private void OnBuyButtonClick()
    {
        switch (_shopItemView.ItemInfo.CashType)
        {
            case CashType.Coin:
                Wallet.Take(_shopItemView.ItemInfo.Price);
                _itemUnlocker.Visit(_shopItemView.ItemInfo);
                SelectItem();
                _shopItemView.UnLock();
                YandexSDK.Instance.Save();
                break;

            case CashType.VideoAd:
#if !UNITY_EDITOR && UNITY_WEBGL
                YandexSDK.Instance.ShowVideoAd(() =>
                {
                    _itemUnlocker.Visit(_shopItemView.ItemInfo);
                    SelectItem();
                    _shopItemView.UnLock();
                    YandexSDK.Instance.Save();
                });
                break;
#endif
                _itemUnlocker.Visit(_shopItemView.ItemInfo);
                SelectItem();
                _shopItemView.UnLock();
                YandexSDK.Instance.Save();
                break;

            case CashType.Yan:
                YandexSDK.Instance.MuteAudio(true);
#if !UNITY_EDITOR && UNITY_WEBGL
                Billing.PurchaseProduct(_shopItemView.ItemInfo.YanId.ToString(), (purchasedProduct) =>
                {
                    Billing.ConsumeProduct(purchasedProduct.id, () =>
                    {
                        if (_shopItemView.ItemInfo.ItemType == ItemType.Soft)
                        {
                            SoftItemInfo softItemInfo = _shopItemView.ItemInfo as SoftItemInfo;
                            Wallet.Add(softItemInfo.Reward);
                        }
                        else
                        {
                            _itemUnlocker.Visit(_shopItemView.ItemInfo);
                            SelectItem();
                            _shopItemView.UnLock();
                            YandexSDK.Instance.Save();
                        }

                        YandexSDK.Instance.MuteAudio(YandexSDK.Instance.Data.IsMute);
                    });
                });
#endif

#if UNITY_EDITOR
                if (_shopItemView.ItemInfo.ItemType == ItemType.Soft)
                {
                    SoftItemInfo softItemInfo = _shopItemView.ItemInfo as SoftItemInfo;
                    Wallet.Add(softItemInfo.Reward);
                }
                else
                {
                    _itemUnlocker.Visit(_shopItemView.ItemInfo);
                    SelectItem();
                    _shopItemView.UnLock();
                    YandexSDK.Instance.Save();
                }
#endif
                break;
        }
    }

    private void OnSelecltedButtonClick()
    {
        SelectItem();
        YandexSDK.Instance.Save();
    }

    private void OnSkinButtonClick()
    {
        _skinButton.Select();
        _objectButton.UnSelect();
        _trailButton.UnSelect();
        _animalButton.UnSelect();
        _softButton.UnSelect();

        _shopPanel.Show(_shopContent.SkinItemInfos.Cast<ItemInfo>());
    }

    private void OnObjectButtonClick()
    {
        _skinButton.UnSelect();
        _objectButton.Select();
        _trailButton.UnSelect();
        _animalButton.UnSelect();
        _softButton.UnSelect();

        _shopPanel.Show(_shopContent.ObjectItemInfos.Cast<ItemInfo>());
    }

    private void OnTrailButtonClick()
    {
        _skinButton.UnSelect();
        _objectButton.UnSelect();
        _trailButton.Select();
        _animalButton.UnSelect();
        _softButton.UnSelect();

        _shopPanel.Show(_shopContent.TrailItemInfos.Cast<ItemInfo>());
    }

    private void OnAnimalButtonClick()
    {
        _skinButton.UnSelect();
        _objectButton.UnSelect();
        _trailButton.UnSelect();
        _animalButton.Select();
        _softButton.UnSelect();

        _shopPanel.Show(_shopContent.AnimalItemInfos.Cast<ItemInfo>());
    }

    private void OnSoftButtonClick()
    {
        _skinButton.UnSelect();
        _objectButton.UnSelect();
        _trailButton.UnSelect();
        _animalButton.UnSelect();
        _softButton.Select();

        _shopPanel.Show(_shopContent.SoftItemInfos.Cast<ItemInfo>());
    }

    private void SelectItem()
    {
        _itemSelector.Visit(_shopItemView.ItemInfo);
        _shopPanel.Select(_shopItemView);
        ShowSelectedButton();
    }

    private void ShowBuyButton(ItemInfo itemInfo)
    {
        _buyButton.Initialize(itemInfo);
        _buyButton.gameObject.SetActive(true);
        HideSelectedButton();
        HideSelectionButton();
    }

    private void ShowSelectionButton()
    {
        _selectionButton.gameObject.SetActive(true);
        HideBuyButton();
        HideSelectedButton();
    }

    private void ShowSelectedButton()
    {
        _selectedText.gameObject.SetActive(true);
        HideSelectionButton();
        HideBuyButton();
    }

    private void HideBuyButton() => _buyButton.gameObject.SetActive(false);

    private void HideSelectionButton() => _selectionButton.gameObject.SetActive(false);

    private void HideSelectedButton() => _selectedText.gameObject.SetActive(false);
}
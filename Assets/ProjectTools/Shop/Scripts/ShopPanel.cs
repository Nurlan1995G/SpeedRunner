using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    [SerializeField] private Transform _parent;
    [SerializeField] private ShopItemViewFactory _shopItemViewFactory;

    private OpenItemChecker _openItemChecker;
    private SelectedItemChecker _selectedItemChecker;
    private List<ShopItemView> _shopItemsView = new List<ShopItemView>();

    public event Action<ShopItemView> ItemViewClicked;

    public void Initialize(OpenItemChecker openItemChecker, SelectedItemChecker selectedItemChecker)
    {
        _openItemChecker = openItemChecker;
        _selectedItemChecker = selectedItemChecker;
    }

    public void Select(ShopItemView shopItemView)
    {
        foreach (var shopItem in _shopItemsView)
            shopItem.UnSelect();

        shopItemView.Select();
    }

    public void Show(IEnumerable<ItemInfo> itemInfos)
    {
        Clear();

        foreach (ItemInfo itemInfo in itemInfos)
        {
            ShopItemView shopItemView = _shopItemViewFactory.Get(itemInfo, _parent, _openItemChecker);
            shopItemView.Click += OnClick;
            shopItemView.UnSelect();
            shopItemView.UnHighlight();

            _openItemChecker.Visit(shopItemView.ItemInfo);

            if (_openItemChecker.IsOpened)
            {
                _selectedItemChecker.Visit(shopItemView.ItemInfo);

                if (_selectedItemChecker.IsSelected)
                {
                    shopItemView.Select();
                    shopItemView.Highlight();
                    ItemViewClicked?.Invoke(shopItemView);
                }

                shopItemView.UnLock();
            }
            else
            {
                shopItemView.Lock();
            }

            _shopItemsView.Add(shopItemView);
        }
    }

    private void OnClick(ShopItemView shopItemView)
    {
        Highlight(shopItemView);
        ItemViewClicked?.Invoke(shopItemView);
    }

    private void Highlight(ShopItemView shopItemView)
    {
        foreach (var item in _shopItemsView)
            item.UnHighlight();

        shopItemView.Highlight();
    }

    private void Clear()
    {
        foreach (var item in _shopItemsView)
        {
            item.Click -= OnClick;
            Destroy(item.gameObject);
        }

        _shopItemsView.Clear();
    }
}
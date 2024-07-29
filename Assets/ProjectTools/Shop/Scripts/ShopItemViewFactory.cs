using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemViewFactory", menuName = "Shop/NewShopItemViewFactory", order = 1)]
public class ShopItemViewFactory : ScriptableObject
{
    [SerializeField] private ShopItemView _shopItemView;

    public ShopItemView Get(ItemInfo itemInfo, Transform parent, OpenItemChecker openItemChecker)
    {
        ShopItemView shopItemView;

        switch (itemInfo)
        {
            case SkinItemInfo:
                shopItemView = Instantiate(_shopItemView, parent);
                break;
            case ObjectItemInfo:
                shopItemView = Instantiate(_shopItemView, parent);
                break;
            case TrailItemInfo:
                shopItemView = Instantiate(_shopItemView, parent);
                break;
            case AnimalItemInfo:
                shopItemView = Instantiate(_shopItemView, parent);
                break;
            case SoftItemInfo:
                shopItemView = Instantiate(_shopItemView, parent);
                break;
            default:
                throw new ArgumentException(nameof(itemInfo));
        }

        shopItemView.Initialize(itemInfo, openItemChecker);
        return shopItemView;
    }
}

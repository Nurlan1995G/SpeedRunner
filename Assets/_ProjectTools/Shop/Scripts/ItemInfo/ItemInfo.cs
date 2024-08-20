using Sirenix.OdinInspector;
using UnityEngine;

public abstract class ItemInfo : ScriptableObject
{
    public int Id;
    public ItemType ItemType;
    public CashType CashType;
    public int Price;
    public Sprite Icon;
    public GameObject ShopPreview;
    [ShowIf("CashType", CashType.Yan)] public int YanId;
    public bool IsDynamic = true;
}

public enum CashType
{
    Coin, VideoAd, Yan, Win
}

public enum ItemType
{
    Skin, Trail, Animal, Item, Soft
}

public enum ItemState
{
    Locked, Buyed, Selled
}
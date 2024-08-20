public class ItemUnlocker : IShopItemVisitor
{
    public void Visit(ItemInfo itemInfo) 
        =>  YandexSDK.Instance.Data.OpenItem(itemInfo.Id);
}

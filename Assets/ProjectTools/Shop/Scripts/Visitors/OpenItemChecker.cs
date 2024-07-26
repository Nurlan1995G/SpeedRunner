public class OpenItemChecker : IShopItemVisitor
{
    public bool IsOpened { get; private set; }

    public void Visit(ItemInfo itemInfo)
        => IsOpened = YandexSDK.Instance.Data.OpenItemsInfoId.Contains(itemInfo.Id);
}

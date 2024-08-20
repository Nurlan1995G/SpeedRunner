public class ItemSelector : IShopItemVisitor
{
    public void Visit(ItemInfo itemInfo)
    {
        switch (itemInfo)
        {
            case SkinItemInfo:
                YandexSDK.Instance.Data.SelectedSkin = itemInfo.Id;
                break;
            case ObjectItemInfo:
                YandexSDK.Instance.Data.SelectedObject = itemInfo.Id;
                break;
            case TrailItemInfo:
                YandexSDK.Instance.Data.SelectedTrail = itemInfo.Id;
                break;
            case AnimalItemInfo:
                YandexSDK.Instance.Data.SelectedAnimal = itemInfo.Id;
                break;
        }
    }
}
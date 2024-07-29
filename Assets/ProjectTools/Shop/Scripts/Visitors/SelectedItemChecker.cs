public class SelectedItemChecker : IShopItemVisitor
{
    public bool IsSelected { get; private set; }

    public void Visit(ItemInfo itemInfo)
    {
        switch(itemInfo)
        {
            case SkinItemInfo:
                IsSelected = YandexSDK.Instance.Data.SelectedSkin == itemInfo.Id;
                break;
            case ObjectItemInfo:
                IsSelected = YandexSDK.Instance.Data.SelectedObject == itemInfo.Id;
                break;
            case TrailItemInfo:
                IsSelected = YandexSDK.Instance.Data.SelectedTrail == itemInfo.Id;
                break;
            case AnimalItemInfo:
                IsSelected = YandexSDK.Instance.Data.SelectedAnimal == itemInfo.Id;
                break;
        }
    }
}
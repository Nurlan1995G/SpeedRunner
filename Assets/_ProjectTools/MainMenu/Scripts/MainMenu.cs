using System.Linq;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private ShopContent _shopContent;
    [SerializeField] private SkinPlacement _skinPlacement;

    public bool IsInitialize {  get; private set; }

    private void OnEnable()
    {
        if(IsInitialize)
            Draw();
    }

    private void OnDisable() => _skinPlacement.Clear();

    public void Initialize() => IsInitialize = true;

    public void Show() => gameObject.SetActive(true);

    public void Hide() => gameObject.SetActive(false);

    public void Draw()
    {
        SkinItemInfo skinItemInfo = _shopContent.SkinItemInfos.FirstOrDefault(itemInfo => itemInfo.Id == YandexSDK.Instance.Data.SelectedSkin);
        ObjectItemInfo objectItemInfo = _shopContent.ObjectItemInfos.FirstOrDefault(itemInfo => itemInfo.Id == YandexSDK.Instance.Data.SelectedObject);
        AnimalItemInfo animalItemInfo = _shopContent.AnimalItemInfos.FirstOrDefault(itemInfo => itemInfo.Id == YandexSDK.Instance.Data.SelectedAnimal);

        if(skinItemInfo != null)
        _skinPlacement.InstantiateModel(skinItemInfo.MainMenuPreview);

        if (objectItemInfo != null)
            _skinPlacement.InstantiateModel(objectItemInfo.MainMenuPreview);

        if (animalItemInfo != null)
            _skinPlacement.InstantiateModel(animalItemInfo.MainMenuPreview);
    }
}
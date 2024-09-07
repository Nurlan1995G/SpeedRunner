using System.Collections.Generic;
using UnityEngine;

public class SkinHandler : MonoBehaviour
{
    [SerializeField] private Shop _shop;
    [SerializeField] private List<RewardModel> _skins;

    public RewardModel CurrentSkin { get; private set; }

    private void OnEnable()
    {
        _shop.SkinCangedInShop += LoadSkins;
    }

    private void Start() =>
        LoadSkins();

    private void OnDisable() =>
        _shop.SkinCangedInShop -= LoadSkins;

    private void LoadSkins()
    {
        int selectedSkin = YandexSDK.Instance.Data.SelectedSkin;
        int selectedObject = YandexSDK.Instance.Data.SelectedObject;

        Load(selectedSkin,selectedObject, _skins);
    }

    private void Load(int idSkinPlayer, int idHat, List<RewardModel> skins)
    {
        foreach (var skin in skins)
        {
            if (idSkinPlayer == skin.ItemInfo.Id)
            {
                CurrentSkin = skin;
                skin.ChangeState(true);
            }
            else
                skin.ChangeState(false);
        }
    }
}
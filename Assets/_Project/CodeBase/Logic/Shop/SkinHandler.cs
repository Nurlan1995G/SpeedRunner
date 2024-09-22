using Assets._Project.CodeBase.Player.Skin;
using System.Collections.Generic;
using UnityEngine;

public class SkinHandler : MonoBehaviour
{
    [SerializeField] private Shop _shop;
    [SerializeField] private List<PlayerSkin> _skins;
    [SerializeField] private List<RewardModel> _pets;

    public PlayerSkin CurrentSkin { get; private set; }

    private void OnEnable() =>
        _shop.SkinCangedInShop += LoadSkins;
    
    private void Start() =>
        LoadSkins();

    private void OnDisable() =>
        _shop.SkinCangedInShop -= LoadSkins;

    private void LoadSkins()
    {
        int selectedSkin = YandexSDK.Instance.Data.SelectedSkin;
        int selectedTrail = YandexSDK.Instance.Data.SelectedTrail;
        int selestedAnimal = YandexSDK.Instance.Data.SelectedAnimal;

        LoadSkinAndTrail(selectedSkin, selectedTrail);
        Load(selestedAnimal, _pets);
    }

    private void LoadSkinAndTrail(int idSkinPlayer, int idSelectedTrail)
    {
        foreach (var skin in _skins)
        {
            if (idSkinPlayer == skin.ItemInfo.Id)
            {
                CurrentSkin = skin;
                skin.ChangeState(true);
                LoadTrail(skin, idSelectedTrail);
            }
            else
                skin.ChangeState(false);
        }
    }

    private void LoadTrail(PlayerSkin skin, int idSelectedTrail)
    {
        foreach (var trail in skin.Trails)
        {
            if (idSelectedTrail == trail.ItemInfo.Id)
                trail.ChangeState(true);
            else
                trail.ChangeState(false);
        }
    }

    private void Load(int id, List<RewardModel> skins)
    {
        foreach (var skin in skins)
        {
            if (id == skin.ItemInfo.Id)
                skin.ChangeState(true);
            else
                skin.ChangeState(false);
        }
    }
}
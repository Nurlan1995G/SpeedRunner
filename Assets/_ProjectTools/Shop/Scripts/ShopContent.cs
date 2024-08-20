using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopContent", menuName = "NewShopContent", order = 2)]
public class ShopContent : ScriptableObject
{
    [SerializeField][FolderPath] private string _pathSkinConfig;
    [SerializeField][FolderPath] private string _pathObjectConfig;
    [SerializeField][FolderPath] private string _pathTrailConfig;
    [SerializeField][FolderPath] private string _pathAnimalConfig;
    [SerializeField][FolderPath] private string _pathSoftConfig;

    [LabelText("Skins")][SerializeField] private List<SkinItemInfo> _skinItemInfos = new List<SkinItemInfo>();
    [LabelText("Item")][SerializeField] private List<ObjectItemInfo> _objectItemInfos = new List<ObjectItemInfo>();
    [LabelText("Trail")][SerializeField] private List<TrailItemInfo> _trailItemInfos = new List<TrailItemInfo>();
    [LabelText("Animal")][SerializeField] private List<AnimalItemInfo> _animalItemInfos = new List<AnimalItemInfo>();
    [LabelText("Soft")][SerializeField] private List<SoftItemInfo> _softItemInfos = new List<SoftItemInfo>();

    public IEnumerable<SkinItemInfo> SkinItemInfos => _skinItemInfos;
    public IEnumerable<ObjectItemInfo> ObjectItemInfos => _objectItemInfos;
    public IEnumerable<TrailItemInfo> TrailItemInfos => _trailItemInfos;
    public IEnumerable<AnimalItemInfo> AnimalItemInfos => _animalItemInfos;
    public IEnumerable<SoftItemInfo> SoftItemInfos => _softItemInfos;

    [Button]
    private void InitializeId()
    {
        Sorting(SkinItemInfos);
        Sorting(ObjectItemInfos);
        Sorting(TrailItemInfos);
        Sorting(AnimalItemInfos);
        Sorting(SoftItemInfos);
    }

    private void Sorting(IEnumerable<ItemInfo> itemInfos)
    {
        var array = itemInfos.ToArray();

        for (int i = 0; i < array.Length; i++)
        {
            array[i].Id = array[i].GetInstanceID();
        }
    }
}
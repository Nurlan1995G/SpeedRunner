using System.Collections.Generic;
using UnityEngine;

public class BotSkinHendler : MonoBehaviour
{
    [SerializeField] private GameObject _startSkin;
    [SerializeField] private List<BotSkin> _skins;

    public BotSkin CurrentSkin { get; private set; }

    public void EnableRandomSkin()
    {
        _startSkin.SetActive(false);
        CurrentSkin = _skins[Random.Range(0, _skins.Count)];
        CurrentSkin.Enable();
    }
}


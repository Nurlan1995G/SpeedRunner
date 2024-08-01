using Assets.Project.AssetProviders;
using Assets.Project.CodeBase.Player.Respawn;
using Assets.Project.CodeBase.Player.UI;
using System;
using UnityEngine;

public class PlayerView : Character
{
    [SerializeField] private EffectCoin _effectCoin;

    private UIPopup _uiPopup;
    private RespawnSlime _respawn;
    private PositionStaticData _positionStaticData;
    private SoundHandler _soundhandler;

    public Action<PlayerView> PlayerDied;
    private Language _language;

    public void Construct(PositionStaticData positionStaticData, UIPopup uiPopup,
         SoundHandler soundHandler, RespawnSlime respawnSlime, Language language)
    {
        _respawn = respawnSlime;
        _positionStaticData = positionStaticData ?? throw new ArgumentNullException(nameof(positionStaticData));
        _uiPopup = uiPopup ?? throw new ArgumentNullException(nameof(uiPopup));
        _soundhandler = soundHandler ?? throw new ArgumentNullException(nameof(soundHandler));
        _language = language;
    }

    public override void Destroyable()
    {
        PlayerDied?.Invoke(this);
        _soundhandler.PlayLose();
        gameObject.SetActive(false);

        _respawn.SelectAction();
    }

    public void Teleport()
    {
        transform.position = _positionStaticData.InitPlayerPosition;
        gameObject.SetActive(true);
        _soundhandler.PlayWin();
    }

    public override string GetSharkName()
    {
        if (_language == Language.Russian)
        {
            NickName.NickNameText.text = AssetAdress.NickPlayerRu;
            return AssetAdress.NickPlayerRu;
        }
        else if (_language == Language.English)
        {
            NickName.NickNameText.text = AssetAdress.NickPlayerEn;
            return AssetAdress.NickPlayerEn;
        }

        return null;
    }
}

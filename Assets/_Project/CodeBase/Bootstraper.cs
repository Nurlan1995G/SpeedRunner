using Assets._Project.CodeBase.CameraLogic;
using Assets._Project.Config;
using Assets.Project.AssetProviders;
using System.Collections.Generic;
using UnityEngine;

public class Bootstraper : MonoBehaviour
{
    [SerializeField] private PositionStaticData _positionStaticData;
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private Player _player;
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private PlayerJumper _playerJumper;
    [SerializeField] private CameraRotater _cameraRotater;
    [SerializeField] private SoundHandler _soundHandler;
    [SerializeField] private List<BotView> _botViews;
    [SerializeField] private TimerLevel _timerLevel;

    //private IInput _input;
    private Language _language;

    private void Awake()
    {
        CheckLanguage();

        PlayerInput playerInput = new();
        AssetProvider assetProvider = new();
        PlayerInputs playerInputs = new(playerInput);
        RotateInput rotateInput = new();

        //InitMobileUI();
        _timerLevel.Construct(_gameConfig.LogicConfig);
        InitPlayer(playerInputs);
        InitCamera(rotateInput);
        InitBot();
    }

    private void CheckLanguage()
    {
        if (Localization.CurrentLanguage == ".ru")
            _language = Language.Russian;
        else
            _language = Language.English;
    }

    private void InitPlayer(PlayerInputs playerInputs)
    {
        _player.Construct(_positionStaticData, _gameConfig.CharacterData, _soundHandler, _language,
            playerInputs, _playerMover, _playerJumper);
    }

    private void InitCamera(RotateInput input) =>
        _cameraRotater.Construct(_gameConfig, input);

    private void InitMobileUI()
    {
        if (Application.isMobilePlatform)
        {
            //_input = new MobileInput();

            //_boostButtonUI.SetMobilePlatform();
            //_moveJostick.gameObject.SetActive(true);
            //_boostButtonUI.gameObject.SetActive(true);
        }
    }

    private void InitBot()
    {
        foreach (var bot in _botViews)
        {
            bot.Construct(_gameConfig.CharacterBotData);
        }
    }
}

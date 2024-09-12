using Assets._Project.CodeBase.CameraLogic;
using Assets._Project.Config;
using Assets.Project.AssetProviders;
using System.Collections.Generic;
using Unity.AI.Navigation;
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
    [SerializeField] private List<BotController> _botControllers;
    [SerializeField] private TimerLevel _timerLevel;
    [SerializeField] private CoroutineRunner _coroutineRunner;
    [SerializeField] private NavMeshSurface _meshSurface;
    [SerializeField] private SkinHandler _skinHandler;
    [SerializeField] private LevelLoader _levelLoader;
    [SerializeField] private CountdownController _countdownController;

    private Language _language;

    private void Awake()
    {
        PlayerInput playerInput = new();
        AssetProvider assetProvider = new();
        PlayerInputs playerInputs = new(playerInput);
        RotateInput rotateInput = new();
        CharacterAnimation characterAnimation = new(_skinHandler, _player);

        //InitMobileUI();
        CheckLanguage();

        InitPlayer(playerInputs, characterAnimation);
        InitCamera(rotateInput);
        InitCoroutine();
        InitBot(characterAnimation);
        
        _timerLevel.Construct(_gameConfig.LogicConfig);
        _countdownController.Construct(_timerLevel, _gameConfig.LogicConfig);
        
        InitLevelLoader();
    }

    private void InitLevelLoader()
    {
        _levelLoader.Construct(_timerLevel, _botControllers, _countdownController, _player, _meshSurface);
        _levelLoader.StartLevelSequence();
    }

    private void CheckLanguage()
    {
        if (Localization.CurrentLanguage == ".ru")
            _language = Language.Russian;
        else
            _language = Language.English;
    }

    private void InitPlayer(PlayerInputs playerInputs, CharacterAnimation characterAnimation)
    {
        _player.Construct(_positionStaticData, _gameConfig.CharacterData, _soundHandler,
            playerInputs, _playerMover, _playerJumper, _skinHandler, characterAnimation);
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

    private void InitCoroutine() =>
        _coroutineRunner.Initialize();

    private void InitBot(CharacterAnimation characterAnimation)
    {
        foreach (var bot in _botViews)
        {
            bot.Construct(_gameConfig.CharacterBotData);
        }

        foreach (var botController in _botControllers)
        {
            botController.Construct(_gameConfig.BotControllerData);
        }
    }
}

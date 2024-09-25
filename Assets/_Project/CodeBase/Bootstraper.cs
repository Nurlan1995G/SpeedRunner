using Assets._Project.CodeBase.CameraLogic;
using Assets._Project.CodeBase.Logic.Move;
using Assets._Project.Config;
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
    [SerializeField] private MoveJoystick _moveJoystick;

    [SerializeField] private List<BotView> _botViews;
    [SerializeField] private List<BotController> _botControllers;

    [SerializeField] private TimerLevel _timerLevel;
    [SerializeField] private CoroutineRunner _coroutineRunner;
    [SerializeField] private NavMeshSurface _meshSurface;
    [SerializeField] private SkinHandler _skinHandler;
    [SerializeField] private LevelLoader _levelLoader;
    [SerializeField] private CountdownController _countdownController;
    [SerializeField] private Finish _finish;
    [SerializeField] private GameActivator _gameActivator;

    private Language _language;
    private RaceManager _raceManager;

    private void Awake()
    {
        InitializeComponents();
        CheckLanguage();
        InitCoroutine();
        InitBots();
        InitMobileUI();
        InitLevelLoader();
    }

    private void CheckLanguage() =>
        _language = Localization.CurrentLanguage == ".ru" ? Language.Russian : Language.English;

    private void InitializeComponents()
    {
        PlayerInput playerInput = new();
        RotateInput rotateInput = new();
        PlayerAnimation characterAnimation = new(_skinHandler, _player);
        PlayerInputs playerInputs = new(playerInput);
       _raceManager = new RaceManager(_positionStaticData);

        _player.Construct(_positionStaticData, _gameConfig.CharacterData, _soundHandler, playerInputs, _playerMover, _playerJumper, _skinHandler, characterAnimation);
        _cameraRotater.Construct(_gameConfig, rotateInput);
        _finish.Construct(_raceManager);
    }

    private void InitCoroutine() => 
        _coroutineRunner.Initialize();

    private void InitBots()
    {
        foreach (var bot in _botViews)
            bot.Construct(_gameConfig.CharacterBotData);

        foreach (var botController in _botControllers)
            botController.Construct(_gameConfig.BotControllerData, _gameActivator);
    }

    private void InitLevelLoader()
    {
        _timerLevel.Construct(_gameConfig.LogicConfig, _gameActivator);
        _countdownController.Construct(_timerLevel, _gameConfig.LogicConfig, _language, _gameActivator);
        _levelLoader.Construct(_timerLevel, _botControllers, _botViews, _countdownController, _player, _meshSurface, _raceManager);
        _levelLoader.StartLevelSequence();
    }

    private void InitMobileUI()
    {
        if (Application.isMobilePlatform)
            _moveJoystick.gameObject.SetActive(true);
    }
}
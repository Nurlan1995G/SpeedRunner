using Assets._Project.CodeBase.CameraLogic;
using Assets._Project.CodeBase.Logic.Move;
using Assets._Project.Config;
using Assets.Project.AssetProviders;
using Assets.Project.CodeBase.Player.Respawn;
using Assets.Project.CodeBase.Player.UI;
using Assets.Project.CodeBase.SharkEnemy.Factory;
using System.Collections.Generic;
using UnityEngine;

public class Bootstraper : MonoBehaviour
{
    [SerializeField] private PositionStaticData _positionStaticData;
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private List<SpawnPointEnemyBot> _spawnPoints;
    [SerializeField] private CameraRotater _cameraRotater;
    [SerializeField] private UIPopup _uiPopup;
    [SerializeField] private TopCharacterUI _topSharksUI;
    [SerializeField] private SoundHandler _soundHandler;

    //private IInput _input;
    private Language _language;

    private void Awake()
    {
        CheckLanguage();

        AssetProvider assetProvider = new AssetProvider();
        TopCharacterManager topSharksManager = new TopCharacterManager();
        FactoryCharacter factoryShark = new FactoryCharacter(assetProvider);
        RespawnSlime respawnSlime = new RespawnSlime(_uiPopup, _playerView);
        PlayerInput playerInput = new PlayerInput();
        RotateInput rotateInput = new RotateInput();

        //InitMobileUI();
        WriteSpawnPoint(factoryShark, topSharksManager);
        InitPlayer(playerInput, respawnSlime);
        InitCamera(rotateInput);
        InitTopUI(topSharksManager);
    }

    private void CheckLanguage()
    {
        if (Localization.CurrentLanguage == ".ru")
            _language = Language.Russian;
        else
            _language = Language.English;
    }

    private void WriteSpawnPoint(FactoryCharacter factoryCharacter, TopCharacterManager topCharacterManager)
    {
        foreach (SpawnPointEnemyBot spawnPoint in _spawnPoints)
            spawnPoint.Construct(factoryCharacter, _positionStaticData, _playerView, _gameConfig, topCharacterManager, _language);
    }

    private void InitPlayer(PlayerInput playerInput, RespawnSlime respawnSlime)
    {
        _playerView.Construct(_positionStaticData, _uiPopup, _soundHandler, respawnSlime, _language);
        _playerMover.Construct(_gameConfig.PlayerData, playerInput);
    }

    private void InitCamera(RotateInput input) =>
        _cameraRotater.Construct(_gameConfig, input);

    private void InitTopUI(TopCharacterManager topCharacterManager) =>
        _topSharksUI.Construct(topCharacterManager);

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
}

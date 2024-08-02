using Assets._Project.CodeBase.CameraLogic;
using Assets._Project.Config;
using Assets.Project.AssetProviders;
using Assets.Project.CodeBase.SharkEnemy.Factory;
using System.Collections.Generic;
using UnityEngine;

public class Bootstraper : MonoBehaviour
{
    [SerializeField] private PositionStaticData _positionStaticData;
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private Player _playerView;
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private List<SpawnPointEnemyBot> _spawnPoints;
    [SerializeField] private CameraRotater _cameraRotater;
    [SerializeField] private SoundHandler _soundHandler;
    [SerializeField] private PlayerJumper _playerJumper;

    //private IInput _input;
    private Language _language;

    private void Awake()
    {
        CheckLanguage();

        AssetProvider assetProvider = new AssetProvider();
        FactoryCharacter factoryShark = new FactoryCharacter(assetProvider);
        PlayerInput playerInput = new PlayerInput();
        RotateInput rotateInput = new RotateInput();

        //InitMobileUI();
        WriteSpawnPoint(factoryShark);
        InitPlayer(playerInput);
        InitCamera(rotateInput);
    }

    private void CheckLanguage()
    {
        if (Localization.CurrentLanguage == ".ru")
            _language = Language.Russian;
        else
            _language = Language.English;
    }

    private void WriteSpawnPoint(FactoryCharacter factoryCharacter)
    {
        foreach (SpawnPointEnemyBot spawnPoint in _spawnPoints)
            spawnPoint.Construct(factoryCharacter, _positionStaticData, _playerView, _gameConfig, _language);
    }

    private void InitPlayer(PlayerInput playerInput)
    {
        _playerView.Construct(_positionStaticData, _gameConfig.CharacterData, _soundHandler, _language);
        _playerMover.Construct(_gameConfig.CharacterData, playerInput);
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
}

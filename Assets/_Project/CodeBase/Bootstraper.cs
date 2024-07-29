using System.Collections.Generic;
using UnityEngine;

public class Bootstraper : MonoBehaviour
{
    //[SerializeField] private SpawnerFood _spawnerFood;
    //[SerializeField] private PositionStaticData _positionStaticData;
    //[SerializeField] private GameConfig _gameConfig;
    //[SerializeField] private PlayerView _playerView;
    //[SerializeField] private PlayerMover _playerMover;
    //[SerializeField] private List<SpawnPointEnemyBot> _spawnPoints;
    //[SerializeField] private CameraRotater _cameraRotater;
    //[SerializeField] private ConfigFood _configFood;
    //[SerializeField] private UIPopup _uiPopup;
    //[SerializeField] private BoostButtonUI _boostButtonUI;
    //[SerializeField] private TopSlimeUI _topSharksUI;
    //[SerializeField] private SoundHandler _soundHandler;
    //[SerializeField] private MoveJostick _moveJostick;

    private Language _language;

    private void Awake()
    {
        CheckLanguage();

        /*AssetProvider assetProvider = new AssetProvider();
        ServesSelectTypeFood random = new ServesSelectTypeFood(_configFood);
        TopSharksManager topSharksManager = new TopSharksManager();
        FactoryShark factoryShark = new FactoryShark(assetProvider);
        RespawnSlime respawnSlime = new RespawnSlime(_uiPopup, _playerView);
        PlayerInput playerInput = new PlayerInput();
        RotateInput rotateInput = new RotateInput();
        ScoreLevelBarFoodManager scoreLevelBarFoodManager = new(_gameConfig.CameraRotateData.HideDistance,
            _cameraRotater.transform, _spawnerFood);*/

        /*InitSpawner(assetProvider, random);
        WriteSpawnPoint(factoryShark, topSharksManager);
        InitPlayer(topSharksManager, respawnSlime, playerInput);
        InitCamera(rotateInput, scoreLevelBarFoodManager);
        InitTopUI(topSharksManager);
        InitMobileUI();*/
    }

    private void CheckLanguage()
    {
        if (Localization.CurrentLanguage == ".ru")
            _language = Language.Russian;
        else
            _language = Language.English;
    }

    /*private void InitSpawner(AssetProvider assetProvider, ServesSelectTypeFood random) =>
        _spawnerFood.Construct(new FoodFactory(_configFood, assetProvider), random, _playerView, _configFood);

    private void WriteSpawnPoint(FactoryShark factoryShark, TopSharksManager topSharksManager)
    {
        foreach (SpawnPointEnemyBot spawnPoint in _spawnPoints)
            spawnPoint.Construct(factoryShark, _positionStaticData, _playerView, _spawnerFood, _gameConfig, topSharksManager, _language);
    }

    private void InitPlayer(TopSharksManager topSharksManager, RespawnSlime respawnSlime,
        PlayerInput playerInput)
    {
        _playerView.Construct(_positionStaticData, _gameConfig, _uiPopup, _boostButtonUI, _soundHandler, respawnSlime, playerInput, _language);
        _playerView.Init(topSharksManager);
        _playerMover.Construct(_gameConfig.PlayerData, _boostButtonUI, playerInput);
    }

    private void InitCamera(RotateInput rotateInput, ScoreLevelBarFoodManager scoreLevelBarFoodManager) => 
        _cameraRotater.Construct(_gameConfig, rotateInput, scoreLevelBarFoodManager);

    private void InitTopUI(TopSharksManager topSharksManager) =>
        _topSharksUI.Construct(topSharksManager);

    private void InitMobileUI()
    {
        if (Application.isMobilePlatform)
        {
            _boostButtonUI.SetMobilePlatform();
            _moveJostick.gameObject.SetActive(true);
            _boostButtonUI.gameObject.SetActive(true);
        }
    }*/
}

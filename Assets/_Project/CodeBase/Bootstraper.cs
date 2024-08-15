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
    [SerializeField] private Player _player;
    [SerializeField] private List<SpawnPointEnemyBot> _spawnPoints;
    [SerializeField] private CameraRotater _cameraRotater;
    [SerializeField] private SoundHandler _soundHandler;
    [SerializeField] private Animator _playerAnimation;
    [SerializeField] private FlagPoint _flagPoint;

    //private IInput _input;
    private Language _language;

    private void Awake()
    {
        CheckLanguage();

        AssetProvider assetProvider = new();
        FactoryCharacter factoryShark = new(assetProvider);
        PlayerInput playerInput = new();
        RotateInput rotateInput = new();
        CharacterAnimation characterAnimation = new(_playerAnimation);

        //InitMobileUI();
        //WriteSpawnPoint(factoryShark);
        InitPlayer(playerInput, characterAnimation);
        InitCamera(rotateInput);
        _flagPoint.Construct(_positionStaticData);
    }

    private void CheckLanguage()
    {
        if (Localization.CurrentLanguage == ".ru")
            _language = Language.Russian;
        else
            _language = Language.English;
    }

   /* private void WriteSpawnPoint(FactoryCharacter factoryCharacter)
    {
        foreach (SpawnPointEnemyBot spawnPoint in _spawnPoints)
            spawnPoint.Construct(factoryCharacter, _positionStaticData, _character, _gameConfig, _language);
    }*/

    private void InitPlayer(PlayerInput playerInput, CharacterAnimation characterAnimation)
    {
        _player.Construct(_positionStaticData, _gameConfig, _soundHandler, _language,
            playerInput, characterAnimation);
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

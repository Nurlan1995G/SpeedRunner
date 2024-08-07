using Assets._Project.CodeBase.CameraLogic;
using Assets._Project.Config;
using Assets.Project.AssetProviders;
using Assets.Project.CodeBase.SharkEnemy.Factory;
using Assets.ProjectLesson2.Scripts.Character;
using System.Collections.Generic;
using UnityEngine;

public class Bootstraper : MonoBehaviour
{
    [SerializeField] private PositionStaticData _positionStaticData;
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private Character _character;
    [SerializeField] private List<SpawnPointEnemyBot> _spawnPoints;
    [SerializeField] private CameraRotater _cameraRotater;
    [SerializeField] private SoundHandler _soundHandler;
    [SerializeField] private Animator _playerAnimation;

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
        InitCamera(rotateInput);
        InitPlayer(playerInput, characterAnimation);
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
        _character.Construct(playerInput, _gameConfig, characterAnimation);
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

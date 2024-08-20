using Assets._Project.Config;
using Assets.Project.AssetProviders;
using Assets.Project.CodeBase.SharkEnemy.Factory;
using UnityEngine;

public class SpawnPointEnemyBot : MonoBehaviour
{
    private FactoryCharacter _factoryShark;
    private PositionStaticData _sharkPositionStaticData;
    private Player _playerView;
    private CharacterData _characterData;
    private Language _language;

    public void Construct(FactoryCharacter factoryShark, PositionStaticData sharkPositionStaticData,
        Player playerView, GameConfig gameConfig, Language language)
    { 
        _factoryShark = factoryShark;
        _sharkPositionStaticData = sharkPositionStaticData;
        _playerView = playerView;
        _characterData = gameConfig.CharacterData;
        _language = language;
    }

    private void Update()
    {
        FindMissingSharks();
    }

    private void FindMissingSharks()
    {
        foreach (var sharkTag in AssetAdress.SlimeBotsTag)
        {
            GameObject shark = StaticClassLogic.FindObject(sharkTag);

            if (shark == null)
                RespawnBotShark(sharkTag);
        }
    }

    private void RespawnBotShark(string sharkTag)
    {
        Vector3 position;
        string sharkEnemy;
        string nickName;

        if (sharkTag == "Shark1")
        {
            position = _sharkPositionStaticData.InitSharkOnePosition;
            sharkEnemy = AssetAdress.SlimeEnemy1;
        }
        else if (sharkTag == "Shark2")
        {
            position = _sharkPositionStaticData.InitSharkTwoPosition;
            sharkEnemy = AssetAdress.SlimeEnemy2;
        }
        else if (sharkTag == "Shark3")
        {
            position = _sharkPositionStaticData.InitSharkThreePosition;
            sharkEnemy = AssetAdress.SlimeEnemy3;
        }
        else if (sharkTag == "Shark4")
        {
            position = _sharkPositionStaticData.InitSharkFourPosition;
            sharkEnemy = AssetAdress.SlimeEnemy4;
        }
        else if (sharkTag == "Shark5")
        {
            position = _sharkPositionStaticData.InitSharkFivePosition;
            sharkEnemy = AssetAdress.SlimeEnemy5;
        }
        else if (sharkTag == "Shark6")
        {
            position = _sharkPositionStaticData.InitSharkSixPosition;
            sharkEnemy = AssetAdress.SlimeEnemy6;
        }
        else if (sharkTag == "Shark7")
        {
            position = _sharkPositionStaticData.InitSharkSevenPosition;
            sharkEnemy = AssetAdress.SlimeEnemy7;
        }
        else if (sharkTag == "Shark8")
        {
            position = _sharkPositionStaticData.InitSharkEightPosition;
            sharkEnemy = AssetAdress.SlimeEnemy8;
        }
        else if (sharkTag == "Shark9")
        {
            position = _sharkPositionStaticData.InitSharkNinePosition;
            sharkEnemy = AssetAdress.SlimeEnemy9;
        }
        else
            return;

        BotView botShark = CreateSharkScene(position, sharkEnemy);
        botShark.Construct(_characterData);
    }

    private BotView CreateSharkScene(Vector3 positionShark, string sharkEnemy)
    {
        BotView botShark = _factoryShark.CreateSharkEnemy(sharkEnemy, positionShark);
        return botShark;
    }
}

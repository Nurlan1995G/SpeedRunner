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
            nickName = GetNickname(1);
        }
        else if (sharkTag == "Shark2")
        {
            position = _sharkPositionStaticData.InitSharkTwoPosition;
            sharkEnemy = AssetAdress.SlimeEnemy2;
            nickName = GetNickname(2);
        }
        else if (sharkTag == "Shark3")
        {
            position = _sharkPositionStaticData.InitSharkThreePosition;
            sharkEnemy = AssetAdress.SlimeEnemy3;
            nickName = GetNickname(3);
        }
        else if (sharkTag == "Shark4")
        {
            position = _sharkPositionStaticData.InitSharkFourPosition;
            sharkEnemy = AssetAdress.SlimeEnemy4;
            nickName = GetNickname(4);
        }
        else if (sharkTag == "Shark5")
        {
            position = _sharkPositionStaticData.InitSharkFivePosition;
            sharkEnemy = AssetAdress.SlimeEnemy5;
            nickName = GetNickname(5);
        }
        else if (sharkTag == "Shark6")
        {
            position = _sharkPositionStaticData.InitSharkSixPosition;
            sharkEnemy = AssetAdress.SlimeEnemy6;
            nickName = GetNickname(6);
        }
        else if (sharkTag == "Shark7")
        {
            position = _sharkPositionStaticData.InitSharkSevenPosition;
            sharkEnemy = AssetAdress.SlimeEnemy7;
            nickName = GetNickname(7);
        }
        else if (sharkTag == "Shark8")
        {
            position = _sharkPositionStaticData.InitSharkEightPosition;
            sharkEnemy = AssetAdress.SlimeEnemy8;
            nickName = GetNickname(8);
        }
        else if (sharkTag == "Shark9")
        {
            position = _sharkPositionStaticData.InitSharkNinePosition;
            sharkEnemy = AssetAdress.SlimeEnemy9;
            nickName = GetNickname(9);
        }
        else
            return;

        BotSlimeView botShark = CreateSharkScene(position, sharkEnemy);
        botShark.Construct(_characterData);
        botShark.SetNickname(nickName);
    }

    private string GetNickname(int index)
    {
        if (_language == Language.Russian)
        {
            return index switch
            {
                1 => AssetAdress.NickBotRu1,
                2 => AssetAdress.NickBotRu2,
                3 => AssetAdress.NickBotRu3,
                4 => AssetAdress.NickBotRu4,
                5 => AssetAdress.NickBotRu5,
                6 => AssetAdress.NickBotRu6,
                7 => AssetAdress.NickBotRu7,
                8 => AssetAdress.NickBotRu8,
                9 => AssetAdress.NickBotRu9,
                _ => "Unknown"
            };
        }
        else
        {
            return index switch
            {
                1 => AssetAdress.NickBotEn1,
                2 => AssetAdress.NickBotEn2,
                3 => AssetAdress.NickBotEn3,
                4 => AssetAdress.NickBotEn4,
                5 => AssetAdress.NickBotEn5,
                6 => AssetAdress.NickBotEn6,
                7 => AssetAdress.NickBotEn7,
                8 => AssetAdress.NickBotEn8,
                9 => AssetAdress.NickBotEn9,
                _ => "Unknown"
            };
        }
    }

    private BotSlimeView CreateSharkScene(Vector3 positionShark, string sharkEnemy)
    {
        BotSlimeView botShark = _factoryShark.CreateSharkEnemy(sharkEnemy, positionShark);
        return botShark;
    }
}

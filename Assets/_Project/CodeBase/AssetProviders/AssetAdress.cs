using System.Collections.Generic;

namespace Assets.Project.AssetProviders
{
    public static class AssetAdress
    {
        public const string ScoreLevel = "Score/Level";
        public const string PlayerShark = "Player/Shark";
        public const string SlimeEnemy1 = "SlimeEnemies/SlimeEnemyBot1";
        public const string SlimeEnemy2 = "SlimeEnemies/SlimeEnemyBot2";
        public const string SlimeEnemy3 = "SlimeEnemies/SlimeEnemyBot3";
        public const string SlimeEnemy4 = "SlimeEnemies/SlimeEnemyBot4";
        public const string SlimeEnemy5 = "SlimeEnemies/SlimeEnemyBot5";
        public const string SlimeEnemy6 = "SlimeEnemies/SlimeEnemyBot6";
        public const string SlimeEnemy7 = "SlimeEnemies/SlimeEnemyBot7";
        public const string SlimeEnemy8 = "SlimeEnemies/SlimeEnemyBot8";
        public const string SlimeEnemy9 = "SlimeEnemies/SlimeEnemyBot9";

        public static readonly List<string> SlimeBotsTag = new List<string> { "Shark1", "Shark2", "Shark3", "Shark4","Shark5", "Shark6", "Shark7", "Shark8", "Shark9" };
        public const string PlayerTag = "Player";

        public const string RuNamePath = "Assets/_ProjectTools/Chat/RuName.txt";
        public const string EnNamePath = "Assets/_ProjectTools/Chat/EnName.txt";
        public const string RU = ".ru";
        public const string TimerFormat = @"mm\:ss\.ff";
    }
}
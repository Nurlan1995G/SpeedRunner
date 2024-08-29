using System;
using UnityEngine;

namespace Assets._Project.Config
{
    [CreateAssetMenu(fileName = "Config", menuName = "Config/Data")]
    public class GameConfig : ScriptableObject
    {
        public CharacterData CharacterData;
        public CharacterBotData CharacterBotData;
        public CameraRotateData CameraRotateData;
        public LogicConfig LogicConfig;
    }

    [Serializable]
    public class CharacterData
    {
        [Header("Speed")]
        public float MoveSpeed;
        public float RotateSpeed;
        [Header("Other")]
        public float JumpGravity = 1.5f;
        public float FallGravity = 2.5f;
        public float HeightJump;
        public float JumpStep = 1.5f;
        public float JumpDuration;
        [Header("Boost")]
        public float BoostMultiplier = 2f;
        public float BoostDuration = 0.5f;
        public float BoostHeightUp = 1.2f;
        public float BoostWaitTime = 0.5f;
    }

    [Serializable]
    public class CharacterBotData
    {
        [Header("Speed")]
        public float BotMoveSpeed;
        public float BotRotateSpeed;
        [Header("Other")]
        public float BotHeightJump;
        public float BotJumpStep = 1.5f;
        public float BotJumpDuration;
        public float BotRangeRandomMoving = 20;
        [Header("Boost")]
        public float BotBoostMultiplier = 2f;
        public float BotBoostDuration = 0.5f;
        public float BotBoostHeightUp = 1.2f;
        public float BotBoostWaitTime = 0.5f;
    }

    [Serializable]
    public class CameraRotateData
    {
        [Header("Mobile")]
        public float RotateSpeedMobileX;
        public float RotateSpeedMobileY;
        [Header("Keyboard")]
        public float RotateSpeedKeyboardX;
        public float RotateSpeedKeyboardY;
        public float MinZoomDistance;
        public float MaxZoomDistance;
        public float ZoomStep;
        public float HideDistance;
    }

    [Serializable]
    public class LogicConfig
    {
        public float Timer;
    }
}

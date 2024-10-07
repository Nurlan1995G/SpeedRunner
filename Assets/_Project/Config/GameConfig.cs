using System;
using UnityEngine;

namespace Assets._Project.Config
{
    [CreateAssetMenu(fileName = "Config", menuName = "Config/Data")]
    public class GameConfig : ScriptableObject
    {
        public PlayerData CharacterData;
        public BotAgentData BotAgentData;
        public BotControllerData BotControllerData;
        public CameraRotateData CameraRotateData;
        public LogicConfig LogicConfig;
    }

    [Serializable]
    public class PlayerData
    {
        [Header("Speed")]
        public float MoveSpeed;
        public float RotateSpeed;
        [Header("Other")]
        public float JumpGravity = 1.5f;
        public float FallGravity = 2.5f;
        public float MaxFallGravitySpeed;

        public float FallDelay = 0.5f;
        public float DelayMovement = 5f;

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
    public class BotAgentData
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
    public class BotControllerData
    {
        [Header("Speed")]
        public float MoveSpeed;
        [Range(50, 60)]public float MinMoveSpeed;
        [Range(50, 60)] public float MaxMoveSpeed;
        public float RotateSpeed;
        [Header("Other")]
        public float JumpGravity = 80f;
        public float MaxFallGravitySpeed = 50f;
        public float ClimbDuration = 10f;
        [Header("Jump")]
        public float JumpForce = 60f;
        public float JumpTrampoline = 3f;
        public float BoostUp = 2f;
        [Header("Boost")]
        public float BoostMultiplier = 2f;
        public float BoostDuration = 0.5f;
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
        public float MinZoomDistanceMidle;
        public float MaxZoomDistanceMidle;
        public float MinZoomDistanceBottom;
        public float MaxZoomDistanceBottom;
        public float ZoomStep;
        public float HideDistance;
    }

    [Serializable]
    public class LogicConfig
    {
        public float Timer;
        public int CountdownControllerTime = 3;
    }
}

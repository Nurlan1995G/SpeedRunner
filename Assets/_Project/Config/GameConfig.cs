using System;
using UnityEngine;

namespace Assets._Project.Config
{
    [CreateAssetMenu(fileName = "Config", menuName = "Config/Data")]
    public class GameConfig : ScriptableObject
    {
        public CharacterData CharacterData;
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
        public float Gravity;
        public float HeightJump;
        public float JumpStep = 1.5f;
        public float JumpDuration;
        public float NormalizedJumpTimeMax;
        [Header("Boost")]
        public float BoostMultiplier = 2f;
        public float BoostDuration = 0.5f;
        public float BoostHeightUp = 1.2f;
        public float BoostWaitTime = 0.5f;
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

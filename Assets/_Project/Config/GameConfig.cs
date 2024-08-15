using System;
using UnityEngine;

namespace Assets._Project.Config
{
    [CreateAssetMenu(fileName = "Config", menuName = "Config/Data")]
    public class GameConfig : ScriptableObject
    {
        public CharacterData CharacterData;
        public CameraRotateData CameraRotateData;
    }

    [Serializable]
    public class CharacterData
    {
        public float MoveSpeed;
        public float RotateSpeed;
        public float Gravity;
        public float HeightJump;
        public float JumpTime;
        public float BoostMultiplier = 2f;
        public float BoostDuration = 0.5f;

        public float BaseGrafity =>
            2f * HeightJump / (JumpTime * JumpTime);
    }

    [Serializable]
    public class CameraRotateData
    {
        public float RotateSpeedKeyboardX;
        public float RotateSpeedKeyboardY;
        public float RotateSpeedMobileX;
        public float RotateSpeedMobileY;
        public float MinZoomDistance;
        public float MaxZoomDistance;
        public float ZoomStep;
        public float HideDistance;
    }
}

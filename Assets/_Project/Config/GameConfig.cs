using System;
using UnityEngine;

namespace Assets._Project.Config
{
    [CreateAssetMenu(fileName = "Config", menuName = "Config/Data")]
    public class GameConfig : ScriptableObject
    {
        public PlayerData PlayerData;
        public CameraRotateData CameraRotateData;
        public CharacterBotData CharacterBotData;
    }

    [Serializable]
    public class PlayerData
    {
        public float MoveSpeed;
        public float RotateSpeed;
        public float Gravity;
        public float HeightJump;
    }

    [Serializable]
    public class CharacterBotData
    {
        public float MoveSpeed;
        public float RotateSpeed;
        public float Gravity;
        public float HeightJump;
        internal float MinimalDistanceToObject;
    }

    [Serializable]
    public class CameraRotateData
    {
        public float RotateSpeedPC;
        public float RotateSpeedMobile;
        public float MinZoomDistance;
        public float MaxZoomDistance;
        public float ZoomStep;
        public float HideDistance;
    }
}

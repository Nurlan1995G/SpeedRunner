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

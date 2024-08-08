using System;
using UnityEngine;

namespace Assets._Project.Config
{
    [CreateAssetMenu(fileName = "Config", menuName = "Config/Data")]
    public class GameConfig : ScriptableObject
    {
        public CharacterData CharacterData;
        public CameraRotateData CameraRotateData;
        public AirbornCharacterData AirbornCharacterData;
        public RunningCharacterData RunningCharacterData;
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
    public class RunningCharacterData
    {
        public float MoveSpeed;
        public float RotateSpeed;
    }

    [Serializable]
    public class AirbornCharacterData
    {
        public JumpingData JumpingData;
        public float SpeedAirborn;
        public float Gravity;

        public float BaseGrafity =>
            2f * JumpingData.MaxHeight / (JumpingData.TimeToReachMaxHeight * JumpingData.TimeToReachMaxHeight);
    }

    [Serializable]
    public class JumpingData
    {
        public float MaxHeight;
        public float TimeToReachMaxHeight;

        public float StartYVelosity => 2 * MaxHeight / TimeToReachMaxHeight;
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

using System;
using UnityEngine;

namespace Assets.ProjectLesson2.Scripts.Character.StateMashine.State.Config
{
    [Serializable] 
    public class AirbornStateConfig
    {
        [field: SerializeField] public JumpingStateConfig JumpingStateConfig { get; private set; }
        [field: SerializeField, Range(0,10)] public float Speed { get; private set; }

        public float BaseGrafity =>
            2f * JumpingStateConfig.MaxHeight / (JumpingStateConfig.TimeToReachMaxHeight * JumpingStateConfig.TimeToReachMaxHeight);
    }
}

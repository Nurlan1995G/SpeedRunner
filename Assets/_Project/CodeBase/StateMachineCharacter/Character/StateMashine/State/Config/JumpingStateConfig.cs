using System;
using UnityEngine;

namespace Assets.ProjectLesson2.Scripts.Character.StateMashine.State.Config
{
    [Serializable]
    public class JumpingStateConfig
    {
        [field: SerializeField, Range(0, 10)] public float MaxHeight { get; private set; }
        [field: SerializeField, Range(0, 10)] public float TimeToReachMaxHeight { get; private set; }

        public float StartYVelosity => 2 * MaxHeight / TimeToReachMaxHeight;
    }
}

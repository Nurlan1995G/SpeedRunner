using System;
using UnityEngine;

namespace Assets.ProjectLesson2.Scripts.Character.StateMashine.State.Config
{
    [Serializable]
    public class RunningStateConfig
    {
        [field: SerializeField, Range(0,10)] public float Speed { get; private set; }
    }
}

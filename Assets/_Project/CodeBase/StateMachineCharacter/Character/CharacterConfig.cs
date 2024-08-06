using Assets.ProjectLesson2.Scripts.Character.StateMashine.State.Config;
using UnityEngine;

namespace Assets.ProjectLesson2.Scripts.Character
{
    [CreateAssetMenu(fileName = "CharacterConfig", menuName = "Config/CharacterConfig")]
    public class CharacterConfig : ScriptableObject
    {
        [field: SerializeField] public RunningStateConfig RunningStateConfig { get; private set; }
        [field: SerializeField] public AirbornStateConfig AirbornStateConfig { get; private set; }
    }
}

using Assets._Project.Config;
using Assets.ProjectLesson2.Scripts.Character.StateMashine;
using UnityEngine;

namespace Assets.ProjectLesson2.Scripts.Character
{
    [RequireComponent(typeof(CharacterController))]
    public class Character : MonoBehaviour
    {
        [field: SerializeField] public GroundChecker GroundChecker { get; private set; }
        [field: SerializeField] public CharacterController CharacterController { get; private set; }

        private CharacterStateMachine _stateMachine;

        public Vector3 Velocity { get; set; }

        public GameConfig GameConfig { get; private set; }
        public PlayerInput PlayerInput { get; private set; }
        public CharacterAnimation CharacterAnimation {  get; private set; }

        public void Construct(PlayerInput playerInput, GameConfig gameConfig, CharacterAnimation characterAnimation)
        {
            PlayerInput = playerInput;
            GameConfig = gameConfig;
            CharacterAnimation = characterAnimation;
            _stateMachine = new CharacterStateMachine(this);
        }

        private void Update()
        {
            _stateMachine.HandleInput();

            _stateMachine.Update();
        }

        private void OnEnable() => PlayerInput.Enable();

        private void OnDisable() => PlayerInput.Disable();
    }
}

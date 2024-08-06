using Assets.ProjectLesson2.Scripts.Character.StateMashine;
using UnityEngine;

namespace Assets.ProjectLesson2.Scripts.Character
{
    [RequireComponent(typeof(CharacterController))]
    public class Character : MonoBehaviour
    {
        [field: SerializeField] public CharacterView CharacterView {  get; private set; }
        [field: SerializeField] public CharacterConfig Config { get; private set; }
        [field: SerializeField] public GroundChecker GroundChecker { get; private set; }

        private CharacterStateMachine _stateMachine;

        public PlayerInput PlayerInput { get; private set; }
        public CharacterController CharacterController { get; private set; }

        private void Awake()
        {
            CharacterView.Initialize();
            CharacterController = GetComponent<CharacterController>();
            PlayerInput = new PlayerInput();
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

using Assets.ProjectLesson2.Scripts.Character.StateMashine.State.Airborn;
using static UnityEngine.InputSystem.InputAction;

namespace Assets.ProjectLesson2.Scripts.Character.StateMashine.State.Grounded
{
    public class GroundedState : MovementState
    {
        private readonly GroundChecker _groundCheck;

        public GroundedState (ISwitchState switchState, StateMashineData stateMashineData, Character character) : base(stateMashineData, switchState, character)
        {
            _groundCheck = character.GroundChecker;
        }

        public override void Enter()
        {
            base.Enter();

            CharacterView.StartGrounded();
        }

        public override void Exit()
        {
            base.Exit();

            CharacterView.StopGrounded();
        }

        public override void Update()
        {
            base.Update();

            if (_groundCheck.IsTouches == false)
                SwitchState.SwitchState<FallingState>();
        }

        protected override void AddInputActionsCallbacks()
        {
            base.AddInputActionsCallbacks();

            PlayerInput.Player.Jump.started += OnJumpKeyPressed;
        }

        protected override void RemoveInputActionsCallback()
        {
            base.RemoveInputActionsCallback();

            PlayerInput.Player.Jump.started -= OnJumpKeyPressed;
        }

        private void OnJumpKeyPressed(CallbackContext obj) => 
            SwitchState.SwitchState<JumpingState>();
    }
}

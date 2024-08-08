using Assets.ProjectLesson2.Scripts.Character.StateMashine.State.Airborn;
using UnityEngine;
using UnityEngine.InputSystem;

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
           // Debug.Log("GroundedState - Enter");

            CharacterAnimation.StartGrounded();
        }

        public override void Exit()
        {
            base.Exit();
            //Debug.Log("GroundedState - Exit");
            CharacterAnimation.StopGrounded();
        }

        public override void Update()
        {
            base.Update();

            //Debug.Log("GroundedState - Update");

            if (_groundCheck.IsTouches == false)
                SwitchState.SwitchState<FallingState>();
        }

        protected override void AddInputActionsCallbacks()
        {
            base.AddInputActionsCallbacks();
            //Debug.Log("GroundedState - AddInputActionsCallbacks = Jump подсписка");
            PlayerInput.Player.Jump.started += OnJumpKeyPressed;
        }

        protected override void RemoveInputActionsCallback()
        {
            base.RemoveInputActionsCallback();
            //Debug.Log("GroundedState - RemoveInputActionsCallback = Jump отписка");
            PlayerInput.Player.Jump.started -= OnJumpKeyPressed;
        }

        private void OnJumpKeyPressed(InputAction.CallbackContext context)
        {
            Debug.Log("GroundedState - OnJumpKeyPressed");
            SwitchState.SwitchState<JumpingState>();
        }
    }
}

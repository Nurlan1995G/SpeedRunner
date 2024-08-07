using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.ProjectLesson2.Scripts.Character.StateMashine.State
{
    public class MovementState : IState
    {
        protected readonly StateMashineData StateMashineData;
        protected readonly ISwitchState SwitchState;

        private readonly Character _character;

        public MovementState(StateMashineData stateMashineData, ISwitchState switchState, Character character)
        {
            StateMashineData = stateMashineData;
            SwitchState = switchState;
            _character = character;
        }

        protected PlayerInput PlayerInput => _character.PlayerInput;
        protected CharacterController CharacterController => _character.CharacterController;
        protected CharacterAnimation CharacterAnimation => _character.CharacterAnimation;

        public virtual void Enter()
        {
            CharacterAnimation.StartMovement(); 
            AddInputActionsCallbacks();    
        }

        public virtual void Exit()
        {
            CharacterAnimation.StopMovement();  
            RemoveInputActionsCallback(); 
        }

        public virtual void HandleInput()
        {
            Vector2 inputVector = ReadInput();
            StateMashineData.XInput = inputVector.x;
            StateMashineData.XVelocity = inputVector.x * StateMashineData.Speed;
        }

        public virtual void Update()
        {
            Vector3 velocity = GetConvertedVelocity();

            CharacterController.Move(velocity * Time.deltaTime);
            _character.transform.rotation = GetRotationFrom(velocity); 
        }

        protected virtual void AddInputActionsCallbacks()
        {
            PlayerInput.Player.Move.performed += OnMovePerformed;
            PlayerInput.Player.Move.canceled += OnMoveCanceled;
        }

        protected virtual void RemoveInputActionsCallback()
        {
            PlayerInput.Player.Move.performed -= OnMovePerformed;
            PlayerInput.Player.Move.canceled -= OnMoveCanceled;
        }

        protected bool IsHorizontalInputZero() => StateMashineData.XInput == 0;

        private Quaternion GetRotationFrom(Vector3 velocity)
        {
            if (velocity.x > 0)
                return Quaternion.LookRotation(Vector3.right);

            if (velocity.x < 0)
                return Quaternion.LookRotation(Vector3.left);

            return _character.transform.rotation;
        }

        private Vector3 GetConvertedVelocity() =>
            new Vector3(StateMashineData.XVelocity, StateMashineData.YVelocity, 0);

        private Vector2 ReadInput() =>
            PlayerInput.Player.Move.ReadValue<Vector2>();

        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            Vector2 direction = context.ReadValue<Vector2>();
            Move(direction);
        }

        private void OnMoveCanceled(InputAction.CallbackContext context)
        {
            StateMashineData.XInput = 0;
            StateMashineData.XVelocity = 0;
        }

        private void Move(Vector2 direction)
        {
            Vector3 newDirection = new Vector3(direction.x, 0, direction.y);
            Quaternion cameraRotationY = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);

            MoveCharacter(newDirection, cameraRotationY);
            RotateCharacter(newDirection, cameraRotationY);
        }

        private void MoveCharacter(Vector3 moveDirection, Quaternion cameraRotation)
        {
            Vector3 finalDirection = (cameraRotation * moveDirection).normalized;

            CharacterController.Move(finalDirection * _character.GameConfig.RunningCharacterData
                .MoveSpeed * Time.deltaTime);
        }

        private void RotateCharacter(Vector3 moveDirection, Quaternion cameraRotation)
        {
            if (Vector3.Angle(_character.transform.forward, moveDirection) > 0)
            {
                Vector3 finalDirection = (cameraRotation * moveDirection).normalized;
                Quaternion targetRotation = Quaternion.LookRotation(finalDirection);

                _character.transform.rotation = Quaternion.Lerp(_character.transform.rotation, targetRotation, _character.GameConfig.RunningCharacterData.RotateSpeed * Time.deltaTime);
            }
        }
    }
}

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
            Debug.Log("MovementState - Enter");
            CharacterAnimation.StartMovement(); 
            AddInputActionsCallbacks();    
        }

        public virtual void Exit()
        {
            Debug.Log("MovementState - Exit");
            CharacterAnimation.StopMovement();  
            RemoveInputActionsCallback(); 
        }

        public virtual void HandleInput()
        {
            Debug.Log("MovementState - HandleInput");
            Vector2 direction = PlayerInput.Player.Move.ReadValue<Vector2>();
            Move(direction);
        }

        public virtual void Update()
        {
            Debug.Log("MovementState - Update");

            Vector3 velocity = GetConvertedVecloity();

            CharacterController.Move(velocity * Time.deltaTime);
        }

        protected virtual void AddInputActionsCallbacks()
        {
            Debug.Log("MovementState - AddInputActionsCallbacks");
        }

        protected virtual void RemoveInputActionsCallback()
        {
            Debug.Log("MovementState - RemoveInputActionsCallback");
        }

        protected bool IsHorizontalInputZero() => StateMashineData.XInput == 0;

        private Vector3 GetConvertedVecloity() =>
            new Vector3(StateMashineData.XVelocity, StateMashineData.YVelocity, 0);

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

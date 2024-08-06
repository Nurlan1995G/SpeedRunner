using UnityEngine;

namespace Assets.ProjectLesson2.Scripts.Character.StateMashine.State
{
    public class MovementState : IState
    {
        protected readonly StateMashineData StateMashineData;
        protected readonly ISwitchState SwitchState;

        private Character _character;

        public MovementState(StateMashineData stateMashineData, ISwitchState switchState, Character character)
        {
            StateMashineData = stateMashineData;
            SwitchState = switchState;
            _character = character;
        }

        protected PlayerInput PlayerInput => _character.PlayerInput;
        protected CharacterController CharacterController => _character.CharacterController;
        protected CharacterView CharacterView => _character.CharacterView;
        private Quaternion TurnRight => new Quaternion(0, 0, 0, 0);
        private Quaternion TurnLeft => Quaternion.Euler(0, 180, 0);

        public virtual void Enter()
        {
            CharacterView.StartMovement();

            AddInputActionsCallbacks();
        }

        public virtual void Exit()
        {
            CharacterView.StopMovement();

            RemoveInputActionsCallback();
        }

        public virtual void HandleInput()
        {
            StateMashineData.XInput = ReadHorizontalInput();
            StateMashineData.XVelocity = StateMashineData.XInput * StateMashineData.Speed;
        }

        public virtual void Update()
        {
            Vector3 velocity = GetConvertedVecloity();

            CharacterController.Move(velocity * Time.deltaTime);
            _character.transform.rotation = GetRotationFrom(velocity);
        }

        protected virtual void AddInputActionsCallbacks() { }

        protected virtual void RemoveInputActionsCallback() { }

        protected bool IsHorizontalInputZero() => StateMashineData.XInput == 0;

        private Quaternion GetRotationFrom(Vector3 velocity)
        {
            if (velocity.x > 0)
                return TurnRight;

            if (velocity.x < 0)
                return TurnLeft;

            return _character.transform.rotation;
        }

        private Vector3 GetConvertedVecloity() => 
            new Vector3(StateMashineData.XVelocity, StateMashineData.YVelocity, 0);

        private float ReadHorizontalInput() => 
            PlayerInput.Player.Move.ReadValue<float>();
    }
}

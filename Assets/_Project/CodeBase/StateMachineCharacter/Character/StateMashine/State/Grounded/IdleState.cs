using UnityEngine;

namespace Assets.ProjectLesson2.Scripts.Character.StateMashine.State.Grounded
{
    public class IdleState : GroundedState
    {
        private readonly Character _character;

        public IdleState(ISwitchState switchState, StateMashineData stateMashineData, Character character) : base(switchState, stateMashineData, character)
        {
            _character = character;
        }

        public override void Enter()
        {
            base.Enter();
            CharacterAnimation.StartIdle();
            _character.Velocity = new Vector3(_character.Velocity.x, _character.Velocity.y, _character.Velocity.z);
        }

        public override void Exit()
        {
            base.Exit();
            CharacterAnimation.StopIdle();
        }

        public override void Update()
        {
            base.Update();

            if (IsHorizontalInputZero() == false)
            {
                Debug.Log("IdleState => Run");
                SwitchState.SwitchState<RunningState>();
            }
        }
    }
}

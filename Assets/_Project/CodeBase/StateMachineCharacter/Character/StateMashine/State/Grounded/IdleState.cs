using UnityEngine;

namespace Assets.ProjectLesson2.Scripts.Character.StateMashine.State.Grounded
{
    public class IdleState : GroundedState
    {
        public IdleState(ISwitchState switchState, StateMashineData stateMashineData, Character character) : base(switchState, stateMashineData, character)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("IdleState - Enter");
            CharacterAnimation.StartIdle();
        }

        public override void Exit()
        {
            base.Exit();
            Debug.Log("IdleState - Exit");
            CharacterAnimation.StopIdle();
        }

        public override void Update()
        {
            base.Update();

            Debug.Log("IdleState - Update");

            if (IsHorizontalInputZero())
                return;

            SwitchState.SwitchState<RunningState>();
        }
    }
}

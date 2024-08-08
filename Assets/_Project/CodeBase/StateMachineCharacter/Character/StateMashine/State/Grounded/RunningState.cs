using Assets._Project.Config;
using UnityEngine;

namespace Assets.ProjectLesson2.Scripts.Character.StateMashine.State.Grounded
{
    public class RunningState : GroundedState
    {
        private CharacterData _config;

        public RunningState(ISwitchState switchState, StateMashineData stateMashineData, Character character) : base(switchState, stateMashineData, character)
        {
            _config = character.GameConfig.CharacterData;
        }

        public override void Enter()
        {
            base.Enter();

            Debug.Log("RunningState - Enter");
            CharacterAnimation.StartRunning();
            StateMashineData.Speed = _config.MoveSpeed;
        }

        public override void Exit()
        {
            base.Exit();
            Debug.Log("RunningState - Exit");
            CharacterAnimation.StopRunning();
        }

        public override void Update()
        {
            base.Update();

            Debug.Log("RunningState - Update");
            if (IsHorizontalInputZero())
                SwitchState.SwitchState<IdleState>();
        }
    }
}

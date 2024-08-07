using Assets._Project.Config;

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

            CharacterAnimation.StartRunning();
            StateMashineData.Speed = _config.MoveSpeed;
        }

        public override void Exit()
        {
            base.Exit();

            CharacterAnimation.StopRunning();
        }

        public override void Update()
        {
            base.Update();

            if (IsHorizontalInputZero())
                SwitchState.SwitchState<IdleState>();
        }
    }
}

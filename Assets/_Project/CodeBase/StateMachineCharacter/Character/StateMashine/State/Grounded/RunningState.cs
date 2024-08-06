using Assets.ProjectLesson2.Scripts.Character.StateMashine.State.Config;

namespace Assets.ProjectLesson2.Scripts.Character.StateMashine.State.Grounded
{
    public class RunningState : GroundedState
    {
        private readonly RunningStateConfig _config;

        public RunningState(ISwitchState switchState, StateMashineData stateMashineData, Character character) : base(switchState, stateMashineData, character)
        {
            _config = character.Config.RunningStateConfig;
        }

        public override void Enter()
        {
            base.Enter();

            CharacterView.StartRunning();
            StateMashineData.Speed = _config.Speed;
        }

        public override void Exit()
        {
            base.Exit();

            CharacterView.StopRunning();
        }

        public override void Update()
        {
            base.Update();

            if (IsHorizontalInputZero())
                SwitchState.SwitchState<IdleState>();
        }
    }
}

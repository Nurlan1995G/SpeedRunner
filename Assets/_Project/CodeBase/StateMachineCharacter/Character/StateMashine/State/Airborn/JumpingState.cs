using Assets.ProjectLesson2.Scripts.Character.StateMashine.State.Config;

namespace Assets.ProjectLesson2.Scripts.Character.StateMashine.State.Airborn
{
    public class JumpingState : AirbornState
    {
        private readonly JumpingStateConfig _config;

        public JumpingState(ISwitchState switchState, StateMashineData stateMashineData, Character character) : base(switchState, stateMashineData, character)
        {
            _config = character.Config.AirbornStateConfig.JumpingStateConfig;
        }

        public override void Enter()
        {
            base.Enter();

            CharacterView.StartJumping();

            StateMashineData.YVelocity = _config.StartYVelosity;
        }

        public override void Exit()
        {
            base.Exit();

            CharacterView.StopJumping();
        }

        public override void Update()
        {
            base.Update();

            if (StateMashineData.YVelocity <= 0)
                SwitchState.SwitchState<FallingState>();
        }
    }
}

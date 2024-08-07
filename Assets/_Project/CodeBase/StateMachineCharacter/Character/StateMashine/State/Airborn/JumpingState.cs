using Assets._Project.Config;

namespace Assets.ProjectLesson2.Scripts.Character.StateMashine.State.Airborn
{
    public class JumpingState : AirbornState
    {
        private JumpingData _config; 

        public JumpingState(ISwitchState switchState, StateMashineData stateMashineData, Character character) : base(switchState, stateMashineData, character)
        {
            _config = character.GameConfig.AirbornCharacterData.JumpingData;
        }

        public override void Enter()
        {
            base.Enter();

            CharacterAnimation.StartJumping();

            StateMashineData.YVelocity = _config.StartYVelosity;
        }

        public override void Exit()
        {
            base.Exit();

            CharacterAnimation.StopJumping();
        }

        public override void Update()
        {
            base.Update();

            if (StateMashineData.YVelocity <= 0)
                SwitchState.SwitchState<FallingState>();
        }
    }
}

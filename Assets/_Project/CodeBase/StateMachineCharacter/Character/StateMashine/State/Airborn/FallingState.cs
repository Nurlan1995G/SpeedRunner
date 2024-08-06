using Assets.ProjectLesson2.Scripts.Character.StateMashine.State.Grounded;

namespace Assets.ProjectLesson2.Scripts.Character.StateMashine.State.Airborn
{
    public class FallingState : AirbornState
    {
        private readonly GroundChecker _groundChecker;

        public FallingState (ISwitchState switchState, StateMashineData stateMashineData, Character character) : base(switchState, stateMashineData, character)
        {
            _groundChecker = character.GroundChecker;
        }

        public override void Enter()
        {
            base.Enter();

            CharacterView.StartFalling();
        }

        public override void Exit()
        {
            base.Exit();

            CharacterView.StopFalling();
        }

        public override void Update()
        {
            base.Update();

            if (_groundChecker.IsTouches)
            {
                StateMashineData.YVelocity = 0;
                
                if(IsHorizontalInputZero())
                    SwitchState.SwitchState<IdleState>();
                else
                    SwitchState.SwitchState<RunningState>();
            }
        }
    }
}

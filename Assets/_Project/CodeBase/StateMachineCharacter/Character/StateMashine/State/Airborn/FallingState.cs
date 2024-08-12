using Assets.ProjectLesson2.Scripts.Character.StateMashine.State.Grounded;

namespace Assets.ProjectLesson2.Scripts.Character.StateMashine.State.Airborn
{
    public class FallingState : AirbornState
    {
        private readonly GroundChecker _groundChecker;
        private readonly Character _character;

        public FallingState (ISwitchState switchState, StateMashineData stateMashineData, Character character) : base(switchState, stateMashineData, character)
        {
            _character = character;
            _groundChecker = character.GroundChecker;
        }

        public override void Enter()
        {
            base.Enter();
            CharacterAnimation.StartFalling();
        }

        public override void Exit()
        {
            base.Exit();
            CharacterAnimation.StopFalling();
        }

        public override void Update()
        {
            base.Update();


            if (_groundChecker.IsTouches)
            {
                _character.Velocity.y = 0;


                if(IsHorizontalInputZero())
                    SwitchState.SwitchState<IdleState>();
                else
                    SwitchState.SwitchState<RunningState>();
            }
        }
    }
}

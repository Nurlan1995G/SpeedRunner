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

            CharacterView.StartIdle();
        }

        public override void Exit()
        {
            base.Exit();

            CharacterView.StopIdle();
        }

        public override void Update()
        {
            base.Update();

            if (IsHorizontalInputZero())
                return;

            SwitchState.SwitchState<RunningState>();
        }
    }
}

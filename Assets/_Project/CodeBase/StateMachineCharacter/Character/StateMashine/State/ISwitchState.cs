namespace Assets.ProjectLesson2.Scripts.Character.StateMashine.State
{
    public interface ISwitchState
    {
        void SwitchState<State>() where State : IState;
    }
}
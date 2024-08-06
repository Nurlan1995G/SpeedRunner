namespace Assets.ProjectLesson2.Scripts.Character.StateMashine.State
{
    public interface IState
    {
        void Enter();
        void Exit();
        void Update();
        void HandleInput();
    }
}

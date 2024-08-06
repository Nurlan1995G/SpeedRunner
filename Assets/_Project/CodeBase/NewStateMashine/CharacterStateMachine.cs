using System.Collections.Generic;
using System.Linq;

namespace Assets._Project.CodeBase.NewStateMashine
{
    public class CharacterStateMachine : ISwitchState
    {
        private List<IState> _states;
        private IState _currentState;

        public CharacterStateMachine()
        {
            _states = new List<IState>
            {

            };

            _currentState = _states[0];
            _currentState.Enter();
        }

        public void Switcher<State>() where State : IState
        {
            IState state = _states.FirstOrDefault(state => state is State);

            _currentState.Exit();
            _currentState = state;
            _currentState.Enter();
        }
    }

    public interface ISwitchState
    {
        void Switcher<State>() where State : IState;
    }

    public interface IState
    {
        void Enter();
        void Exit();
    }
}

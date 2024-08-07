using Assets.ProjectLesson2.Scripts.Character.StateMashine.State;
using Assets.ProjectLesson2.Scripts.Character.StateMashine.State.Airborn;
using Assets.ProjectLesson2.Scripts.Character.StateMashine.State.Grounded;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.ProjectLesson2.Scripts.Character.StateMashine
{
    public class CharacterStateMachine : ISwitchState
    {
        private List<IState> _states;
        private IState _currentState;

        public CharacterStateMachine(Character character)
        {
            StateMashineData stateMashineData = new StateMashineData();
            Debug.Log("CharacerStateMachine = cons");

            _states = new List<IState>
            {
                new IdleState(this, stateMashineData, character),
                new RunningState(this, stateMashineData, character),
                new JumpingState(this, stateMashineData, character),
                new FallingState(this, stateMashineData, character),
            };

            _currentState = _states[0];
            _currentState.Enter();
        }

        public void SwitchState<State>() where State : IState
        {
            IState state = _states.FirstOrDefault(state => state is State);

            _currentState.Exit();
            _currentState = state;
            _currentState.Enter();
        }

        public void HandleInput() => _currentState.HandleInput();

        public void Update() => _currentState.Update();
    }
}
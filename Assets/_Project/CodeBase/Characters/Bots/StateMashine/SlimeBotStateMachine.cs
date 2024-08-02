using Assets._Project.Config;
using Assets.Project.CodeBase.SharkEnemy.StateMashine.Interface;
using Assets.Project.CodeBase.SharkEnemy.StateMashine.State;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Project.CodeBase.SharkEnemy.StateMashine
{
    public class SlimeBotStateMachine 
    {
        private List<IState> _states;
        private IState _currentState;

        public SlimeBotStateMachine(NavMeshAgent agent, CharacterModel sharkModel, CharacterData characterData)
        {
            _states = new List<IState>
            {
                new AgentMoveState(agent,sharkModel, characterData),
            };

            _currentState = _states[0];
        }

        public void Update() => _currentState.Update();
    }
}

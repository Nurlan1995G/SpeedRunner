using UnityEngine.AI;
using UnityEngine;
using Assets.Project.CodeBase.SharkEnemy.StateMashine.Interface;
using Assets._Project.Config;

namespace Assets.Project.CodeBase.SharkEnemy.StateMashine.State
{
    public class AgentMoveState : IState
    {
        protected NavMeshAgent _agent;
        protected CharacterModel _sharkModel;
        private CharacterBotData _characterBotData;
        private DetecterToObject _detecterToObject;

        public AgentMoveState (NavMeshAgent agent, CharacterModel sharkModel, CharacterBotData characterBotData)
        {
            _agent = agent;
            _sharkModel = sharkModel;
            _characterBotData = characterBotData;

            _agent.speed = _characterBotData.MoveSpeed;

            _detecterToObject = new DetecterToObject(this, sharkModel,characterBotData);
        }

        public void MoveTo(Vector3 position, Transform transform)
        {
            _agent.destination = position;
            RotateCharacter(position, transform, _characterBotData.RotateSpeed);
        }

        private void RotateCharacter(Vector3 targetPosition, Transform transform, float rotateSpeed)
        {
            Vector3 targetDirection = (targetPosition - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation,
                rotateSpeed * Time.deltaTime);
        }

        public virtual void Update()
        {
        }
    }
}

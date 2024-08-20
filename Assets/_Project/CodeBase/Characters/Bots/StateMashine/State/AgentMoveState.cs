using UnityEngine.AI;
using UnityEngine;
using Assets.Project.CodeBase.SharkEnemy.StateMashine.Interface;
using Assets._Project.Config;

namespace Assets.Project.CodeBase.SharkEnemy.StateMashine.State
{
    public class AgentMoveState : IState
    {
        protected NavMeshAgent _agent;
        private CharacterData _characterData;

        public AgentMoveState (NavMeshAgent agent, CharacterData characterData)
        {
            _agent = agent;
            _characterData = characterData;
        }

        public void MoveTo(Vector3 position, Transform transform)
        {
            _agent.destination = position;
            RotateCharacter(position, transform, _characterData.RotateSpeed);
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

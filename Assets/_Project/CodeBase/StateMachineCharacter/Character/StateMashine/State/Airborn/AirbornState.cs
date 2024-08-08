using Assets._Project.Config;
using UnityEngine;

namespace Assets.ProjectLesson2.Scripts.Character.StateMashine.State.Airborn
{
    public class AirbornState : MovementState
    {
        private readonly AirbornCharacterData _config;
        private readonly Character _character;

        public AirbornState (ISwitchState switchState, StateMashineData stateMashineData, Character character) : base(stateMashineData, switchState, character)
        {
            _config = character.GameConfig.AirbornCharacterData;
            _character = character;
        }

        public override void Enter()
        {
            base.Enter();
            //Debug.Log("AirbornState - Enter");
            CharacterAnimation.StartAirborn();
        }

        public override void Exit()
        {
            base.Exit();
            //Debug.Log("AirbornState - Exit");
            CharacterAnimation.StopAirborn();
        }

        public override void Update()
        {
            base.Update();

            //Debug.Log("AirbornState - Update");

            GravityHandling();
        }

        private void GravityHandling()
        {
            if (_character.GroundChecker.IsTouches == false)
            {
                //Debug.Log("GravityHandling - isGrounded");
                _character.Velocity.y -= _config.Gravity * Time.deltaTime;
            }
            else
            {
                //Debug.Log("GravityHandling - _character.Velocity.y = -5f");
                _character.Velocity.y = 0f;
            }

            CharacterController.Move(_character.Velocity * Time.deltaTime);
        }
    }
}

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
            CharacterAnimation.StartAirborn();
        }

        public override void Exit()
        {
            base.Exit();
            CharacterAnimation.StopAirborn();
        }

        public override void Update()
        {
            base.Update();

            GravityHandling();
        }

        private void GravityHandling()
        {
            if (_character.GroundChecker.IsTouches == false)
                _character.Velocity.y -= _config.Gravity * Time.deltaTime;

            CharacterController.Move(_character.Velocity * Time.deltaTime);
        }
    }
}

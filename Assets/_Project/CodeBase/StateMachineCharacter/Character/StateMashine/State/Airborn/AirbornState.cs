using Assets.ProjectLesson2.Scripts.Character.StateMashine.State.Config;
using UnityEngine;

namespace Assets.ProjectLesson2.Scripts.Character.StateMashine.State.Airborn
{
    public class AirbornState : MovementState
    {
        private readonly AirbornStateConfig _config;

        public AirbornState (ISwitchState switchState, StateMashineData stateMashineData, Character character) : base(stateMashineData, switchState, character)
        {
            _config = character.Config.AirbornStateConfig;
        }

        public override void Enter()
        {
            base.Enter();

            CharacterView.StartAirborn();

            StateMashineData.Speed = _config.Speed;
        }

        public override void Exit()
        {
            base.Exit();

            CharacterView.StopAirborn();
        }

        public override void Update()
        {
            base.Update();

            StateMashineData.YVelocity -= GetGravityMultiPlayer() * Time.deltaTime;
        }

        protected virtual float GetGravityMultiPlayer() =>
            _config.BaseGrafity;
    }
}

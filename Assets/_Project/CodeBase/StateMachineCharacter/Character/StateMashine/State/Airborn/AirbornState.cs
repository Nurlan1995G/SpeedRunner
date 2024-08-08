using Assets._Project.Config;
using UnityEngine;

namespace Assets.ProjectLesson2.Scripts.Character.StateMashine.State.Airborn
{
    public class AirbornState : MovementState
    {
        private readonly AirbornCharacterData _config;

        public AirbornState (ISwitchState switchState, StateMashineData stateMashineData, Character character) : base(stateMashineData, switchState, character)
        {
            _config = character.GameConfig.AirbornCharacterData;
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("AirbornState - Enter");
            CharacterAnimation.StartAirborn();

            StateMashineData.Speed = _config.SpeedAirborn;
        }

        public override void Exit()
        {
            base.Exit();
            Debug.Log("AirbornState - Exit");
            CharacterAnimation.StopAirborn();
        }

        public override void Update()
        {
            base.Update();

            Debug.Log("AirbornState - Update");

            StateMashineData.YVelocity -= GetGravityMultiPlayer() * Time.deltaTime;
        }

        protected virtual float GetGravityMultiPlayer() =>
            _config.BaseGrafity;
    }
}

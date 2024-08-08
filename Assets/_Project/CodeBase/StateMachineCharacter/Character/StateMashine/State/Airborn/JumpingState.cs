using Assets._Project.Config;
using TMPro;
using UnityEngine;

namespace Assets.ProjectLesson2.Scripts.Character.StateMashine.State.Airborn
{
    public class JumpingState : AirbornState
    {
        private readonly JumpingData _config;
        private readonly Character _character;

        public JumpingState(ISwitchState switchState, StateMashineData stateMashineData, Character character) : base(switchState, stateMashineData, character)
        {
            _config = character.GameConfig.AirbornCharacterData.JumpingData;
            _character = character;
        }

        public override void Enter()
        {
            base.Enter();
            //Debug.Log("JumpingState- Enter");
            CharacterAnimation.StartJumping();
        }

        public override void Exit()
        {
            base.Exit();
            //Debug.Log("JumpingState- Exit");
            CharacterAnimation.StopJumping();
        }

        public override void Update()
        {
            base.Update();
            //Debug.Log("JumpingState- Update");
            if (_character.Velocity.y <= 0)
                SwitchState.SwitchState<FallingState>();
        }
    }
}

using Assets.Project.CodeBase.SharkEnemy.StateMashine.State;
using Assets.Project.CodeBase.SharkEnemy;
using UnityEngine;
using Assets._Project.Config;

public class DetecterToObject
{
    private readonly AgentMoveState _agentMoveState;
    private readonly CharacterModel _sharkModel;
    private readonly CharacterBotData _characterBotData;

    private float _timeLastDetected;
    private float _cooldownTimer;
    private bool _isChasing = true;

    public DetecterToObject( AgentMoveState agentMoveState,  CharacterModel characterModel, CharacterBotData characterBotData)
    {
        _agentMoveState = agentMoveState;
        _sharkModel = characterModel;
        _characterBotData = characterBotData;
    }

    private void GetDelayTime()
    {
        if (!_isChasing && _cooldownTimer > 0)
        {
            _cooldownTimer -= Time.deltaTime;

            if (_cooldownTimer < 0)
                _isChasing = true;
        }
    }
}
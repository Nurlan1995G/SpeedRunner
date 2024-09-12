﻿using Assets._Project.CodeBase.Characters.Interface;
using Assets._Project.Config;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotView : MonoBehaviour, IRespawned
{
    [SerializeField] private FlagPoint _targetPoint;
    [SerializeField] private BotSkinHendler _botSkinHendler;
    [SerializeField] private BehaviourType _behaviourType;

    private BotAgentAnimator _botAnimator;
    private MoveToPoint _moveToFinish;
    private IdleBehavior _idleBehavior;
    private RandomBehaviour _randomMoving;
    private IBehaviour _currentBehaviour;
    private List<IBehaviour> _behaviours;
    private BoostBoxUp _boostBoxUp;
    private Vector3 _respawnPosition;

    private bool _isActivateJetpack = false;

    [field: SerializeField] public GroundChecker GroundChecker { get; private set; }
    [field: SerializeField] public NavMeshAgent Agent { get; private set; }
    [field: SerializeField] public BotNickName Nickname { get; private set; }
    public BotAgentData CharacterBotData { get; private set; }
    public Vector3 StartPosition { get; private set; }


    public event Action Respawned;

    public void Construct(BotAgentData character)
    {
        CharacterBotData = character;
        StartPosition = transform.position;

        InitializeBotBehavior();

        SelectBehaviourType();

        InitStartPosition();
    }

    private void Update()
    {
        _botAnimator?.Update();
    }

    public void ActivateForRace()
    {
        transform.position = StartPosition;
        _respawnPosition = transform.position;
    }

    public void Respawn() =>
        Respawned.Invoke();

    public void ChagePosition()
    {
        Agent.Warp(_respawnPosition);
        ChangeBehaviour(_currentBehaviour);
    }

    public void SetRespawnPosition(Vector3 position) => 
        _respawnPosition = position;

    private void InitializeBotBehavior()
    {
        _botSkinHendler.EnableRandomSkin();
        BotMover botMover = new(this);
        _botAnimator = new BotAgentAnimator(_botSkinHendler.CurrentSkin.Animator, this);
        _randomMoving = new RandomBehaviour(_botAnimator, botMover);
        _moveToFinish = new MoveToPoint(botMover, _targetPoint, _botAnimator);
        _idleBehavior = new IdleBehavior(_botAnimator, botMover);

        _behaviours = new List<IBehaviour>
        {
            _moveToFinish,
            _idleBehavior,
            _randomMoving
        };
    }

    private void InitStartPosition()
    {
        StartPosition = transform.position;
        _respawnPosition = transform.position;
    }

    private void SelectBehaviourType()
    {
        switch (_behaviourType)
        {
            case BehaviourType.Idle:
                ChangeBehaviour(_idleBehavior);
                break;
            case BehaviourType.MoveToPoint:
                ChangeBehaviour(_moveToFinish);
                break;
            case BehaviourType.RandomMoving:
                ChangeBehaviour(_randomMoving);
                break;
        }
    }

    private void ChangeBehaviour(IBehaviour behaviour)
    {
        _currentBehaviour?.Deactivate();
        _currentBehaviour = behaviour;
        _currentBehaviour.Activate();
    }
}

public enum BehaviourType
{
    Idle,
    MoveToPoint,
    RandomMoving
}

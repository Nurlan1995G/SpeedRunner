using Assets._Project.CodeBase.Characters.Interface;
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
    private Vector3 _startPosition;

    [field: SerializeField] public GroundChecker GroundChecker { get; private set; }
    [field: SerializeField] public NavMeshAgent Agent { get; private set; }
    [field: SerializeField] public BotNickName Nickname { get; private set; }
    public BotAgentData BotAgentData { get; private set; }


    public event Action Respawned;

    public void Construct(BotAgentData character)
    {
        BotAgentData = character;
        _startPosition = transform.position;

        InitializeBotBehavior();

        SelectBehaviourType();
    }

    private void Update()
    {
        _botAnimator?.Update();
    }

    public void ChagePosition()
    {
        if (Agent.isOnNavMesh)
        {
            Agent.Warp(_startPosition);
            ChangeBehaviour(_currentBehaviour);
        }
    }

    public void Respawn()
    {
        gameObject.SetActive(false);

        if (Agent.isOnNavMesh)
        {
            transform.position = _startPosition;
            Agent.Warp(_startPosition);
        }

        gameObject.SetActive(true);
    }

    public void ActivateForRace()
    {
    }

    public void StopMovement()
    {
    }

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

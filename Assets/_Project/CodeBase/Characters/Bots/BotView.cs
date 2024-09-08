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

    private BotAnimator _botAnimator;
    private MoveToPoint _moveToFinish;
    private IdleBehavior _idleBehavior;
    private RandomBehaviour _randomMoving;
    private IBehaviour _currentBehaviour;
    private List<IBehaviour> _behaviours;
    private BoostBoxUp _boostBoxUp;

    private bool _isActivateJetpack = false;

    [field: SerializeField] public GroundChecker GroundChecker { get; private set; }
    [field: SerializeField] public NavMeshAgent Agent { get; private set; }
    [field: SerializeField] public BotNickName Nickname { get; private set; }
    public CharacterBotData CharacterBotData { get; private set; }
    public Vector3 RespawnPosition { get; private set; }

    public event Action Respawned;

    public void Construct(CharacterBotData character)
    {
        CharacterBotData = character;

        InitializeBotBehavior();

        SelectBehaviourType();

        RespawnPosition = transform.position;
    }

    private void Update()
    {
        if (Agent.isOnOffMeshLink)
            _isActivateJetpack = true;
        else
            _isActivateJetpack = false;

        _botAnimator?.Update(_isActivateJetpack);
    }

    public void Respawn() =>
        Respawned.Invoke();

    public void ChagePosition()
    {
        Agent.Warp(RespawnPosition);
        ChangeBehaviour(_currentBehaviour);

        /*if (NavMesh.SamplePosition(RespawnPosition, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
        {
            Agent.Warp(hit.position);
            Debug.Log($"Agent {name} деформирован до состояния навмешивания в {hit.position}");
        }
        else
        {
            Debug.LogError($"Agent {name} не удалось преобразовать в навигационную сетку при {RespawnPosition}");
        }

        ChangeBehaviour(_currentBehaviour);*/
    }

    public void SetRespawnPosition(Vector3 position) => 
        RespawnPosition = position;

    private void InitializeBotBehavior()
    {
        _botSkinHendler.EnableRandomSkin();
        BotMover botMover = new(this);
        _botAnimator = new BotAnimator(_botSkinHendler.CurrentSkin.Animator, this);
        _randomMoving = new RandomBehaviour(_botAnimator, botMover);
        _moveToFinish = new MoveToPoint(botMover, _targetPoint, _botAnimator);
        _idleBehavior = new IdleBehavior(_botAnimator, botMover);

        _behaviours = new List<IBehaviour>
        {
            _moveToFinish,
            _idleBehavior,
            _randomMoving
        };

        if (!Agent.isOnNavMesh)
        {
            Debug.LogError($"Agent {name} \r\nпосле инициализации его нет в NavMesh.");
        }
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
        Debug.Log($"Изменение поведения для {this.name} to {behaviour.GetType().Name}");

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

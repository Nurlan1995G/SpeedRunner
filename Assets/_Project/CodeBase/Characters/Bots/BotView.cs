using Assets._Project.CodeBase.Characters.Interface;
using Assets._Project.Config;
using System;
using UnityEngine;
using UnityEngine.AI;

public class BotView : MonoBehaviour, IRespawned
{
    [SerializeField] private FlagPoint _targetPoint;
    [SerializeField] private BotSkinHendler _botSkinHendler;

    private BotAnimator _botAnimator;
    private Player _player;
    private MoveToPoint _moveToFinish;
    private bool _isActivateJetpack = false;

    [field: SerializeField] public NavMeshAgent Agent { get; private set; }
    [field: SerializeField] public BotNickName Nickname { get; private set; }
    public CharacterData CharacterBotData { get; private set; }
    public Vector3 RespawnPosition { get; private set; }

    public event Action Respawned;

    public void Construct(CharacterData character)
    {
        CharacterBotData = character;
        _botSkinHendler.EnableRandomSkin();
        _botAnimator = new(_botSkinHendler.CurrentSkin.Animator, transform);

        BotMover botMover = new(this);
        _moveToFinish = new(botMover, _targetPoint, _botAnimator);
        RespawnPosition = transform.position;

        _moveToFinish.Activate();
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
    }

    public void SetRespawnPosition(Vector3 position)
    {
        RespawnPosition = position;
    }
}

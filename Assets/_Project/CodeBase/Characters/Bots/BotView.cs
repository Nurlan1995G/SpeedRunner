﻿using Assets._Project.CodeBase.Characters.Interface;
using Assets._Project.Config;
using System;
using System.Collections;
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
    private IBehaviour _currentBehaviour;
    private List<IBehaviour> _behaviours;
    private BoostBoxUp _boostBoxUp;

    private bool _isActivateJetpack = false;

    [field: SerializeField] public NavMeshAgent Agent { get; private set; }
    [field: SerializeField] public BotNickName Nickname { get; private set; }
    public CharacterData CharacterBotData { get; private set; }
    public Vector3 RespawnPosition { get; private set; }

    public event Action Respawned;

    public void Construct(CharacterData character)
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
    }

    public void SetRespawnPosition(Vector3 position)
    {
        Debug.Log(RespawnPosition + " - RespawnPosition");
        RespawnPosition = position;
    }

    public void SetBoostBoxUp(BoostBoxUp boostBoxUp)
    {
        if (_boostBoxUp != null)
            _boostBoxUp.BoostJump -= OnBoostJump;

        _boostBoxUp = boostBoxUp;

        _boostBoxUp.BoostJump += OnBoostJump;
    }

    private void InitializeBotBehavior()
    {
        _botSkinHendler.EnableRandomSkin();
        BotMover botMover = new(this);
        _botAnimator = new(_botSkinHendler.CurrentSkin.Animator, transform);
        _moveToFinish = new(botMover, _targetPoint, _botAnimator);
        _idleBehavior = new(_botAnimator, botMover);

        _behaviours = new List<IBehaviour>
        {
            _moveToFinish,
            _idleBehavior
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
        }
    }

    private void ChangeBehaviour(IBehaviour behaviour)
    {
        _currentBehaviour?.Deactivate();
        _currentBehaviour = behaviour;
        _currentBehaviour.Activate();
    }

    private void OnBoostJump()
    {
        StartCoroutine(JumpCoroutine(CharacterBotData.BoostHeightUp * CharacterBotData.JumpStep));
    }

    private IEnumerator JumpCoroutine(float jumpHeight)
    {
        Debug.Log("JumpCoroutine started with height: " + jumpHeight);
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + Vector3.up * jumpHeight;

        Agent.enabled = false; // Отключаем NavMeshAgent временно

        float elapsedTime = 0f;
        float duration = 0.5f; // Время прыжка

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        Agent.enabled = true; // Включаем NavMeshAgent обратно
    }
}

public enum BehaviourType
{
    Idle,
    MoveToPoint
}

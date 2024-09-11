﻿using Assets._Project.CodeBase.Characters.Interface;
using Assets._Project.Config;
using System.Collections;
using UnityEngine;

public class BotController : MonoBehaviour, IRespawned
{
    [SerializeField] private BotMovement _movement;
    [SerializeField] private BotSkinHendler _skinHendler;
    [SerializeField] private PointSpawnZone _currentZone;

    private TargetPoint _currentTarget;
    private PointSpawnZone _previousZone;
    
    private Vector3 _respawnPosition;
    private bool _isAchievedTarget;
    private BotControllerAnimator _botControllerAnimator;
    private Coroutine _speedBoostCoroutine;
    private float _currentSpeed;

    [field: SerializeField] public GroundChecker GroundChecker { get; private set; }
    [field: SerializeField] public CharacterController CharacterController { get; private set; }
    public BotControllerData BotControllerData { get; private set; }

    public void Construct(BotControllerData botControllerData)
    {
        BotControllerData = botControllerData;

        _botControllerAnimator = new BotControllerAnimator(_skinHendler, this, _movement);
        _movement.Construct(this);
        _respawnPosition = transform.position;
        _skinHendler.EnableRandomSkin();
        BotControllerData.MoveSpeed = RandomSpeed();
        _currentSpeed = BotControllerData.MoveSpeed;
        _previousZone = _currentZone;
        
        SelectRandomTargetInCurrentZone();
    }

    private void Update()
    {
        GravityHandling();

        if (_currentZone != null)
            MoveTowardsTarget();

        _botControllerAnimator.HandleAnimations(_movement.MovementSpeed, _movement.Velocity);
    }

    public void SetRespawnPosition(Vector3 position)
    {
        _respawnPosition = position;
        _previousZone = _currentZone;
    }

    public void Respawn()
    {
        gameObject.SetActive(false);
        transform.position = _respawnPosition;
        gameObject.SetActive(true);
        _currentTarget = null;
        _currentZone = _previousZone;

        SelectRandomTargetInCurrentZone();
    }

    public void SetZone(PointSpawnZone zone)
    {
        _currentZone = zone;
        SelectRandomTargetInCurrentZone();
    }

    public void BoostBoxUp() =>
        _movement.Jump(BotControllerData.JumpForce * BotControllerData.BoostUp);

    public void JumpTrigger() =>
        _movement.Jump(BotControllerData.JumpForce);
    
    public void TrampolineJumpUp() => 
        _movement.Jump(BotControllerData.JumpForce * BotControllerData.JumpTrampoline);

    public void ActivateBoostForward()
    {
        if (_speedBoostCoroutine != null)
            StopCoroutine(_speedBoostCoroutine);

        _speedBoostCoroutine = StartCoroutine(SpeedBoostCoroutine(BotControllerData.BoostMultiplier,
                BotControllerData.BoostDuration));
    }

    private void MoveTowardsTarget()
    {
        if (_currentTarget == null)
            return;

        Vector3 direction = (_currentTarget.transform.position - transform.position).normalized;
        _movement.Move(direction, _currentSpeed);
        _movement.Rotate(direction, BotControllerData.RotateSpeed);
    }

    private void GravityHandling() =>
        _movement.ApplyGravity(BotControllerData.JumpGravity, BotControllerData.MaxFallGravitySpeed);

    private void SelectRandomTargetInCurrentZone()
    {
        if (_currentZone == null || _currentZone.TargetPoints.Count == 0)
            return;

        _currentTarget = _currentZone.TargetPoints[Random.Range(0, _currentZone.TargetPoints.Count)];
        _isAchievedTarget = false;
    }

    private float RandomSpeed() => 
        Random.Range(BotControllerData.MinMoveSpeed, BotControllerData.MaxMoveSpeed);

    private IEnumerator SpeedBoostCoroutine(float multiplier, float duration)
    {
        _currentSpeed = BotControllerData.MoveSpeed * multiplier;
        yield return new WaitForSeconds(duration);
        _currentSpeed = BotControllerData.MoveSpeed;
    }
}

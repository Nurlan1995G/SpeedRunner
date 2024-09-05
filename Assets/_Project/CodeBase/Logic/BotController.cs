using Assets._Project.CodeBase.Characters.Interface;
using Assets._Project.Config;
using System;
using System.Collections.Generic;
using UnityEngine;
public class BotController : MonoBehaviour, IRespawned
{
    [SerializeField] private BotMovement _movement;
    [SerializeField] private List<PointSpawnZone> _spawnZones;
    [SerializeField] private BotSkinHendler _skinHendler;
    [SerializeField] private GameConfig _characterBot;

    private TargetPoint _currentTarget;
    private bool _isAchievedTarget;   //достигнута ли цель

    [field: SerializeField] public GroundChecker GroundChecker { get; private set; }
    [field: SerializeField] public CharacterController CharacterController { get; private set; }
    public Vector3 RespawnPosition { get; private set; }

    private void Awake()
    {
        _movement.Construct(this);
        RespawnPosition = transform.position;
    }

    private void Update()
    {
        GravityHandling();
        CheckAndMoveToTarget();
    }

    public void SetRespawnPosition(Vector3 position) =>
        RespawnPosition = position;

    public void Respawn()
    {
        if (_currentTarget != null)
        {
            _currentTarget.Diactivate = false;
            _currentTarget.IsBusy = false;
            _isAchievedTarget = false;
        }

        gameObject.SetActive(false);
        transform.position = RespawnPosition;
        gameObject.SetActive(true);

        if (_currentTarget != null)
        {
            _currentTarget.IsBusy = true;
            MoveTowardsTarget();
        }
    }

    private void CheckAndMoveToTarget()
    {
        if (_currentTarget == null || _isAchievedTarget)
        {
            if (_isAchievedTarget && _currentTarget != null)
            {
                _currentTarget.Diactivate = false;
                _currentTarget.IsBusy = false;

                _currentTarget = null;
                _isAchievedTarget = false;
            }

            if (_currentTarget == null)
            {
                _currentTarget = FindNearestFreePoint();

                if (_currentTarget != null)
                {
                    _currentTarget.IsBusy = true;
                }
            }
        }

        if (_currentTarget != null && !_currentTarget.Diactivate)
        {
            MoveTowardsTarget();
        }
    }

    private void MoveTowardsTarget()
    {
        Vector3 direction = (_currentTarget.transform.position - transform.position).normalized;
        _movement.Move(direction);
        _movement.Rotate(direction, _characterBot.CharacterData.RotateSpeed);

        if (Vector3.Distance(transform.position, _currentTarget.transform.position) < 0.5f)
        {
            _isAchievedTarget = true;
            _currentTarget.Diactivate = true;

            _currentTarget = null;
            _movement.Jump();
        }
    }

    private TargetPoint FindNearestFreePoint()
    {
        TargetPoint nearestPoint = null;
        float nearestDistance = float.MaxValue;

        foreach (var spawnZone in _spawnZones)
        {
            foreach (var point in spawnZone.TargetPoints)
            {
                if (!point.IsBusy)
                {
                    float distance = Vector3.Distance(transform.position, point.transform.position);

                    if (distance < nearestDistance)
                    {
                        nearestDistance = distance;
                        nearestPoint = point;
                    }
                }
            }
        }

        if (nearestPoint != null)
        {
            nearestPoint.IsBusy = true;
        }

        return nearestPoint;
    }

    private void GravityHandling()
    {
        if (!GroundChecker.IsGrounded)
        {
           _movement.ApplyGravity(_characterBot.CharacterData.JumpGravity);
        }
        else
        {
            _movement.ResetVerticalVelocity();
        }
    }
}

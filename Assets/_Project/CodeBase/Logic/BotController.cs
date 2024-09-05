using Assets._Project.Config;
using System.Collections.Generic;
using UnityEngine;
public class BotController : MonoBehaviour
{
    [SerializeField] private BotMovement _movement;
    [SerializeField] private List<PointSpawnZone> _spawnZones;
    [SerializeField] private BotSkinHendler _skinHendler;
    [SerializeField] private GameConfig _characterBot;

    private TargetPoint _currentTarget;
    private bool _isAchievedTarget;

    [field: SerializeField] public GroundChecker GroundChecker { get; private set; }
    [field: SerializeField] public CharacterController CharacterController { get; private set; }

    private void Awake()
    {
        _movement.Construct(this);
    }

    private void Update()
    {
        GravityHandling();

        if (_currentTarget == null || _isAchievedTarget)
        {
            _currentTarget = FindNearestFreePoint();
            _isAchievedTarget = false;

            if (_currentTarget != null)
            {
                _currentTarget.IsBusy = true; 
            }
        }

        if (_currentTarget != null)
        {
            MoveTowardsTarget();
        }
    }

    private void MoveTowardsTarget()
    {
        Vector3 direction = (_currentTarget.transform.position - transform.position).normalized;
        _movement.Move(direction);

        if (Vector3.Distance(transform.position, _currentTarget.transform.position) < 0.5f)
        {
            _isAchievedTarget = true;
            DeactivateZone(_currentTarget.transform.parent.GetComponent<PointSpawnZone>());
            Debug.Log("_isAchievedTarget");
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

    private void DeactivateZone(PointSpawnZone zone)
    {
        Debug.Log("DeactivateZone");

        foreach (var point in zone.TargetPoints)
        {
            point.IsBusy = true;
        }

        _currentTarget = null;
        _isAchievedTarget = false;
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

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PointSpawnZone spawnZone))
        {
            Debug.Log("OnTriggerEnter");
            foreach (var point in spawnZone.TargetPoints)
            {
                point.IsBusy = true;
            }
        }
    }*/
}

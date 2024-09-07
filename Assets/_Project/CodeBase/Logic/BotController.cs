using Assets._Project.CodeBase.Characters.Interface;
using Assets._Project.Config;
using UnityEngine;

public class BotController : MonoBehaviour, IRespawned
{
    [SerializeField] private BotMovement _movement;
    [SerializeField] private BotSkinHendler _skinHendler;
    [SerializeField] private GameConfig _characterBot;
    [SerializeField] private PointSpawnZone _currentZone;

    private BotAnimatorr _botAnimator;
    private TargetPoint _currentTarget;
    private PointSpawnZone _previousZone;
    
    private bool _isAchievedTarget;

    [field: SerializeField] public GroundChecker GroundChecker { get; private set; }
    [field: SerializeField] public CharacterController CharacterController { get; private set; }
    public Vector3 RespawnPosition { get; private set; }

    public void Awake()
    {
        _movement.Construct(this);
        RespawnPosition = transform.position;
        _skinHendler.EnableRandomSkin();
        _botAnimator = new BotAnimatorr(_skinHendler.CurrentSkin.Animator, this);
    }

    private void Start()
    {
        _previousZone = _currentZone;

        SelectRandomTargetInCurrentZone();
    }

    private void Update()
    {
        GravityHandling();

        if (_currentZone != null)
            MoveTowardsTarget();

        _botAnimator?.Update(_isAchievedTarget);
    }

    public void SetRespawnPosition(Vector3 position)
    {
        RespawnPosition = position;
        _previousZone = _currentZone;
    }

    public void Respawn()
    {
        gameObject.SetActive(false);
        transform.position = RespawnPosition;
        gameObject.SetActive(true);
        _currentTarget = null;
        _currentZone = _previousZone;

        SelectRandomTargetInCurrentZone();
    }

    private void MoveTowardsTarget()
    {
        if (_currentTarget == null)
            return;

        Vector3 direction = (_currentTarget.transform.position - transform.position).normalized;
        _movement.Move(direction);
        _movement.Rotate(direction, _characterBot.CharacterData.RotateSpeed);
    }

    private void GravityHandling()
    {
        if (!GroundChecker.IsGrounded)
        {
            _movement.ApplyGravity(_characterBot.CharacterData.JumpGravity, _characterBot.CharacterData.FallDelay);
        }
        else
        {
            _movement.ResetVerticalVelocity();
        }
    }

    private void SelectRandomTargetInCurrentZone()
    {
        if (_currentZone == null || _currentZone.TargetPoints.Count == 0)
            return;

        _currentTarget = _currentZone.TargetPoints[Random.Range(0, _currentZone.TargetPoints.Count)];
        _isAchievedTarget = false;
    }

    public void SetZone(PointSpawnZone zone)
    {
        _currentZone = zone;
        SelectRandomTargetInCurrentZone();
        _movement.Jump();
    }
}

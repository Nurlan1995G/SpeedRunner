using Assets._Project.CodeBase.Characters.Interface;
using Assets._Project.Config;
using UnityEngine;

public class BotController : MonoBehaviour, IRespawned
{
    [SerializeField] private BotMovement _movement;
    [SerializeField] private BotSkinHendler _skinHendler;
    [SerializeField] private PointSpawnZone _currentZone;

    private BotControllerAnimator _botAnimator;
    private TargetPoint _currentTarget;
    private PointSpawnZone _previousZone;
    
    private Vector3 _respawnPosition;
    private bool _isAchievedTarget;
    private CharacterAnimationBot _characterAnimator;

    [field: SerializeField] public GroundChecker GroundChecker { get; private set; }
    [field: SerializeField] public CharacterController CharacterController { get; private set; }
    public BotControllerData BotControllerData { get; private set; }

    public void Construct(BotControllerData botControllerData)
    {
        BotControllerData = botControllerData;

        _characterAnimator = new CharacterAnimationBot(_skinHendler, this, _movement);
        _movement.Construct(this);
        _respawnPosition = transform.position;
        _skinHendler.EnableRandomSkin();
        //_botAnimator = new BotControllerAnimator(_skinHendler.CurrentSkin.Animator, this, _movement);
    }

    private void Start()
    {
        _previousZone = _currentZone;

        SelectRandomTargetInCurrentZone();
    }

    private void Update()
    {
        GravityHandling();

        Debug.Log("IsGrounded - " + GroundChecker.IsGrounded + " - " + gameObject.name);
        Debug.Log("isGrounded - " + CharacterController.isGrounded + " - " + gameObject.name);
        //Debug.Log("IsOnJumpBot - " + GroundChecker.IsOnJumpBot + " - " + gameObject.name);
        //Debug.Log("IsOnBoostUp - " + GroundChecker.IsOnBoostUp + " - " + gameObject.name);

        if (_currentZone != null)
            MoveTowardsTarget();

        //_botAnimator?.Update();
        _characterAnimator.HandleAnimations(_movement.MovementSpeed, _movement.Velocity);
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

    private void MoveTowardsTarget()
    {
        if (_currentTarget == null)
            return;

        Vector3 direction = (_currentTarget.transform.position - transform.position).normalized;
        _movement.Move(direction);
        _movement.Rotate(direction, BotControllerData.RotateSpeed);
    }

    private void GravityHandling()
    {
        if (!GroundChecker.IsGrounded)
            _movement.ApplyGravity(BotControllerData.JumpGravity, BotControllerData.MaxFallGravitySpeed);
        else
            _movement.ResetVerticalVelocity();
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

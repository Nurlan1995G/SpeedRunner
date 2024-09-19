using Assets._Project.Config;
using System.Collections;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private LayerMask wallLayer;  
    [SerializeField] private float wallCheckDistance = 1f; 

    private Player _player;
    private PlayerData _playerData;

    private Vector3 _velocityDirection;
    private Coroutine _speedBoostCoroutine;
    private float _currentSpeed;
    private Camera _camera;
    private BoostBoxUp _boostBoxUp;

    private bool _isClimbing;
    private bool _isDance;

    public void Construct(Player player)
    {
        _player = player;
        _playerData = _player.CharacterData;
        _currentSpeed = _playerData.MoveSpeed;

        _camera = Camera.main;
    }


    private void Update()
    {
        Vector2 moveDirection = _player.PlayerInputs.MoveDirection;

        if (_isClimbing)
        {
            MoveClimbing(moveDirection);

            if (_player.PlayerInputs.JumpTriggered)
                DetachFromWall();
        }
        else
            Move(moveDirection);

        _player.CharacterAnimation.HandleAnimations(moveDirection, _velocityDirection, _isDance, _isClimbing);

        GravityHandling();
    }

    public void TakeJumpDirection(float jumpDirection) => 
        _velocityDirection.y = Mathf.Sqrt(2 * _playerData.JumpGravity * jumpDirection);

    public void ActivateSpeedBoost()
    {
        if (_speedBoostCoroutine != null)
            StopCoroutine(_speedBoostCoroutine);

        _speedBoostCoroutine = StartCoroutine(SpeedBoostCoroutine(_player.CharacterData.BoostMultiplier,
                _player.CharacterData.BoostDuration));
    }

    public void BoostBoxUp() => 
        TakeJumpDirection(_player.CharacterData.BoostHeightUp);

    public void StartClimbing()
    {
        _isClimbing = true;
        _velocityDirection = Vector3.zero;
    }

    public void StopClimbing()
    {
        _isClimbing = false;
    }

    public void SetDance(bool isDance) =>
        _isDance = isDance;

    public void StopMovement()
    {
        _currentSpeed = 0;
        _velocityDirection = Vector3.zero;
    }

    private void DetachFromWall()
    {
        StopClimbing();
        TakeJumpDirection(_playerData.HeightJump / 3);
    }

    private void Move(Vector2 direction)
    {
        if (!_isClimbing)
        {
            Vector3 newDirection = new Vector3(direction.x, 0, direction.y);
            Quaternion cameraRotationY = Quaternion.Euler(0, _camera.transform.eulerAngles.y, 0);

            MoveCharacter(newDirection, cameraRotationY);
            RotateCharacter(newDirection, cameraRotationY);
        }
    }

    private void MoveClimbing(Vector2 direction)
    {
        Vector3 climbDirection = new Vector3(direction.x, 0, direction.y);
        MoveCharacter(climbDirection, Quaternion.identity);
    }

    private void MoveCharacter(Vector3 moveDirection, Quaternion cameraRotation)
    {
        Vector3 finalDirection = (cameraRotation * moveDirection).normalized;

        if (_player.GroundChecker.IsGrounded || _isClimbing)
            _player.CharacterController.Move(finalDirection * _currentSpeed * Time.deltaTime);
        else if (!_isClimbing)
            _player.CharacterController.Move(finalDirection * _currentSpeed / 2 * Time.deltaTime);
    }

    private void RotateCharacter(Vector3 moveDirection, Quaternion cameraRotation)
    {
        if (Vector3.Angle(transform.forward, moveDirection) > 0)
        {
            Vector3 finalDirection = (cameraRotation * moveDirection).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(finalDirection);

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _playerData.RotateSpeed * Time.deltaTime);
        }
    }

    private void GravityHandling()
    {
        if (_isClimbing == false && _player.GroundChecker.IsGrounded == false)
        {
            if (_velocityDirection.y > 0)
                _velocityDirection.y -= _playerData.JumpGravity * Time.deltaTime;
            else
                _velocityDirection.y -= _playerData.FallGravity * Time.deltaTime;

            _velocityDirection.y = Mathf.Max(_velocityDirection.y, -_playerData.MaxFallGravitySpeed);
        }

        _player.CharacterController.Move(_velocityDirection * Time.deltaTime);
    }

    private IEnumerator SpeedBoostCoroutine(float multiplier, float duration)
    {
        _currentSpeed = _playerData.MoveSpeed * multiplier;
        yield return new WaitForSeconds(duration);
        _currentSpeed = _playerData.MoveSpeed;
    }
}

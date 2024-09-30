using Assets._Project.Config;
using System.Collections;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private Player _player;
    private RotateInput _ratateInput;
    private PlayerData _playerData;

    private Vector3 _velocityDirection;
    private Coroutine _speedBoostCoroutine;
    private float _currentSpeed;
    private Camera _camera;
    private BoostBoxUp _boostBoxUp;

    private bool _isClimbing;

    public void Construct(Player player, RotateInput ratateInput)
    {
        _player = player;
        _ratateInput = ratateInput;
        _playerData = _player.CharacterData;
        _currentSpeed = _playerData.MoveSpeed;

        _camera = Camera.main;
    }

    private void Update()
    {
        GravityHandling();
        Vector2 moveDirection = _player.PlayerInputs.MoveDirection;

        if (_isClimbing)
        {
            MoveClimbing(moveDirection);

            if (_player.PlayerInputs.JumpTriggered)
                DetachFromWall();
        }
        else
            Move(moveDirection);

        _player.CharacterAnimation.HandleAnimations(moveDirection, _velocityDirection, _isClimbing);
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

    public void StartClimbing(Quaternion targetRotation)
    {
        _isClimbing = true;
        _velocityDirection = Vector3.zero;
    }

    public void StopClimbing() => 
        _isClimbing = false;

    public void StopMovement()
    {
        _currentSpeed = 0;
        _velocityDirection = Vector3.zero;
        StartCoroutine(DisableMovementCoroutine(_player.CharacterData.DelayMovement));
    }

    public void ResetStartSpeed() => 
        _currentSpeed = _playerData.MoveSpeed;

    private IEnumerator DisableMovementCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        ResetStartSpeed();
    }

    private void DetachFromWall()
    {
        StopClimbing();
        TakeJumpDirection(_playerData.HeightJump / 3);
    }

    private void Move(Vector2 direction)
    {
        Vector3 newDirection = new Vector3(direction.x, 0, direction.y);
        Quaternion cameraRotationY = Quaternion.Euler(0, _camera.transform.eulerAngles.y, 0);

        MoveCharacter(newDirection, cameraRotationY, 1);
        RotateCharacter(newDirection, cameraRotationY);
    }

    private void MoveClimbing(Vector2 direction)
    {
        Vector3 climbDirection = new Vector3(direction.x, 0, 0);
        MoveCharacter(climbDirection, Quaternion.identity, 2);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void MoveCharacter(Vector3 moveDirection, Quaternion cameraRotation, float delaySpeedClimb)
    {
        Vector3 finalDirection = (cameraRotation * moveDirection).normalized;

        if (_player.GroundChecker.IsGrounded || _isClimbing)
            _player.CharacterController.Move(finalDirection * _currentSpeed / delaySpeedClimb * Time.deltaTime);
        else if (!_isClimbing)
            _player.CharacterController.Move(finalDirection * _currentSpeed / 1.8f * Time.deltaTime);
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
            {
                _velocityDirection.y -= _playerData.FallGravity * Time.deltaTime;
            }

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

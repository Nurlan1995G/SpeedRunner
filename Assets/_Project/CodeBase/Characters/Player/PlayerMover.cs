using Assets._Project.Config;
using System.Collections;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private Player _player;
    private CharacterData _playerData;

    private Vector3 _velocityDirection;
    private Coroutine _speedBoostCoroutine;
    private float _currentSpeed;
    private BoostBoxUp _boostBoxUp;

    public void Construct(Player player)
    {
        _player = player;
        _playerData = _player.CharacterData;
        _currentSpeed = _playerData.MoveSpeed;
    }

    private void OnEnable()
    {
        if(_boostBoxUp != null)
            _boostBoxUp.BoostJump += OnBoostJump;
    }


    private void Update()
    {
        GravityHandling();
        Vector2 moveDirection = _player.PlayerInputs.MoveDirection;
        Move(moveDirection);

        HandleAnimations(moveDirection);
    }

    private void OnDisable()
    {
        if(_boostBoxUp != null)
            _boostBoxUp.BoostJump -= OnBoostJump;
    }

    public void TakeJumpDirection(float jumpDirection) => 
        _velocityDirection.y = jumpDirection;

    public void ActivateSpeedBoost()
    {
        if (_speedBoostCoroutine != null)
            StopCoroutine(_speedBoostCoroutine);

        _speedBoostCoroutine = StartCoroutine(SpeedBoostCoroutine(_player.CharacterData.BoostMultiplier,
                _player.CharacterData.BoostDuration));
    }

    public void SetBoostBoxUp(BoostBoxUp boostBoxUp)
    {
        if (_boostBoxUp != null)
            _boostBoxUp.BoostJump -= OnBoostJump;

        _boostBoxUp = boostBoxUp;

        _boostBoxUp.BoostJump += OnBoostJump;
    }

    private void HandleAnimations(Vector2 moveDirection)
    {
        if (_player.GroundChecker.IsGrounded)
        {
            _player.CharacterAnimation.StopFalling();
            _player.CharacterAnimation.StopJumping();

            if (moveDirection != Vector2.zero)
            {
                _player.CharacterAnimation.StartRunning();
                _player.CharacterAnimation.StopIdle();
            }
            else
            {
                _player.CharacterAnimation.StopRunning();
                _player.CharacterAnimation.StartIdle();
            }
        }
        else
        {
            _player.CharacterAnimation.StopRunning();
            _player.CharacterAnimation.StopIdle();

            if (_velocityDirection.y > 0)
            {
                _player.CharacterAnimation.StartJumping();
                _player.CharacterAnimation.StopFalling();
            }
            else
            {
                _player.CharacterAnimation.StopJumping();
                _player.CharacterAnimation.StartFalling();
            }
        }
    }

    private void Move(Vector2 direction)
    {
       Vector3 newDirection = new Vector3(direction.x, 0, direction.y);
        Quaternion cameraRotationY = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);

        MoveCharacter(newDirection, cameraRotationY);
        RotateCharacter(newDirection, cameraRotationY);
    }

    private void MoveCharacter(Vector3 moveDirection, Quaternion cameraRotation)
    {
        Vector3 finalDirection = (cameraRotation * moveDirection).normalized;

        _player.CharacterController.Move(finalDirection * _currentSpeed * Time.deltaTime);
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
        if (_player.GroundChecker.IsGrounded == false)
        {
            _velocityDirection.y -= _playerData.Gravity * Time.deltaTime;
        }

        _player.CharacterController.Move(_velocityDirection * Time.deltaTime);
    }

    private void OnBoostJump() => 
        StartCoroutine(WaitForJumpInput());

    private IEnumerator SpeedBoostCoroutine(float multiplier, float duration)
    {
        _currentSpeed = _playerData.MoveSpeed * multiplier;
        yield return new WaitForSeconds(duration);
        _currentSpeed = _playerData.MoveSpeed;
    }

    private IEnumerator WaitForJumpInput()
    {
        float elapsedTime = 0f;

        while (elapsedTime < _player.CharacterData.BoostWaitTime)
        {
            if (_player.PlayerInputs.JumpTriggered)
            {
                TakeJumpDirection(_player.CharacterData.BoostHeightUp * _player.CharacterData.JumpStep);
                yield break;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        TakeJumpDirection(_player.CharacterData.BoostHeightUp);
    }
}

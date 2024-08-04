using Assets._Project.Config;
using UnityEngine;

public class PlayerJumper : MonoBehaviour
{
    private CharacterController _characterController;
    private PlayerInput _playerInput;
    private CharacterData _playerData;

    private Vector3 _velocity;
    private float _jumpVelocity;

    public void Construct(CharacterData playerData, CharacterController characterController, PlayerInput playerInput)
    {
        _playerData = playerData;
        _playerInput = playerInput;
        _characterController = characterController;

        SetJumpVelocity();
        _playerInput.Enable();
    }

    private void Update()
    {
        HandleJump();
        ApplyGravity();

        _characterController.Move(_velocity * Time.deltaTime);
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void SetJumpVelocity()
    {
        float maxHeightTime = _playerData.JumpTime / 2;
        _playerData.Gravity = (2 * _playerData.HeightJump) / Mathf.Pow(maxHeightTime, 2);
        _jumpVelocity = (2 * _playerData.HeightJump) / maxHeightTime;
    }

    private void HandleJump()
    {
        if (_characterController.isGrounded && _playerInput.Player.Jump.triggered)
        {
            _velocity.y = _jumpVelocity;
        }
    }

    private void ApplyGravity()
    {
        if (!_characterController.isGrounded)
        {
            _velocity.y -= _playerData.Gravity * Time.deltaTime;
        }
    }
}
using Assets._Project.Config;
using UnityEngine;

public class PlayerJumper : MonoBehaviour
{
    private CharacterController _characterController;
    private PlayerMover _playerMover;
    private CharacterData _playerData;
    private PlayerInput _playerInput;

    private Vector3 _velocity;
    private bool _isGrounded;
    private float _startJumpVelocity;

    public void Construct(CharacterData playerData, CharacterController _characterController, PlayerInput playerInput, PlayerMover playerMover)
    {
        _playerData = playerData;
        _playerInput = playerInput;
        _playerMover = playerMover;

        SetJumpVelocity();
       // _playerInput.Enable();
    }

    private void Update()
    {
        Vector2 direction = _playerInput.Player.Jump.ReadValue<Vector2>();

        if (_playerInput.Player.Jump.triggered)
            Jump();
    }

    private void OnDisable()
    {
        //_playerInput.Disable();
    }

    private void SetJumpVelocity()
    {
        float maxHeightTime = _playerData.JumpTime / 2;
        _playerData.Gravity = (2 * _playerData.HeightJump) / Mathf.Pow(maxHeightTime, 2);
        _startJumpVelocity = (2 * _playerData.HeightJump) / maxHeightTime;
    }

    private void Jump()
    {
        if (_characterController.isGrounded)
        {
            _playerMover.TakeJumpDirection(_startJumpVelocity);
        }
    }
}
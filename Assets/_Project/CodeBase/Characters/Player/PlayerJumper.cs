using Assets._Project.Config;
using UnityEngine;

public class PlayerJumper : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    private CharacterData _playerData;

    private Vector3 _velocity;
    private bool _isGrounded;

    public void Construct(CharacterData playerData, CharacterController _characterController)
    {
        _playerData = playerData;
    }

    private void Update()
    {
        _isGrounded = _characterController.isGrounded;

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f; 
        }

        HandleJumpInput();
        ApplyGravity();

        _characterController.Move(_velocity * Time.deltaTime);
    }

    private void HandleJumpInput()
    {
        if (_isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Jump()
    {
        float jumpVelocity = Mathf.Sqrt(_playerData.HeightJump * 2f * _playerData.Gravity);
        _velocity.y = jumpVelocity;
    }

    private void ApplyGravity()
    {
        _velocity.y += _playerData.Gravity * Time.deltaTime; 
    }
}
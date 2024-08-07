using Assets._Project.CodeBase.Logic.Move;
using Assets._Project.Config;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private CharacterController _characterController;
    private PlayerInput _input;
    private CharacterData _playerData;

    private Vector3 _velocityDirection;

    public void Construct(CharacterData playerData, PlayerInput playerInput, CharacterController characterController)
    {
        _playerData = playerData ?? throw new System.ArgumentNullException(nameof(playerData));
        _input = playerInput ?? throw new System.ArgumentNullException(nameof(playerInput));
        _characterController = characterController;
        _input.Enable();
    }

    private void Update()
    {
        GravityHandling();

        Vector2 moveDirection = _input.Player.Move.ReadValue<Vector2>();
        Move(moveDirection);
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    public void TakeJumpDirection(float jumpDirection)
    {
        _velocityDirection.y = jumpDirection;
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

        _characterController.Move(finalDirection * _playerData.MoveSpeed * Time.deltaTime);
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
        if (!_characterController.isGrounded)
        {
            _velocityDirection.y -= _playerData.Gravity * Time.deltaTime;
        }
        else
        {
            _velocityDirection.y = -5f;
        }

        _characterController.Move(_velocityDirection * Time.deltaTime);
    }
}

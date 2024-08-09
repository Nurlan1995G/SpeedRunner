using Assets._Project.Config;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private Player _player;
    private CharacterData _playerData;

    private Vector3 _velocityDirection;

    public void Construct(CharacterData playerData, Player player)
    {
        _player = player;
        _playerData = playerData ?? throw new System.ArgumentNullException(nameof(playerData));
    }

    private void Update()
    {
        GravityHandling();

        Vector2 moveDirection = _player.PlayerInput.Player.Move.ReadValue<Vector2>();
        Move(moveDirection);
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

        _player.CharacterController.Move(finalDirection * _playerData.MoveSpeed * Time.deltaTime);
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
        if (_player.CharacterController.isGrounded == false)
        {
            _velocityDirection.y -= _playerData.Gravity * Time.deltaTime;
        }

        _player.CharacterController.Move(_velocityDirection * Time.deltaTime);
    }
}

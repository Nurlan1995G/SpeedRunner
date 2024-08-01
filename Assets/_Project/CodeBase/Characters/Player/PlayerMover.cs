using Assets._Project.Config;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private GameObject _moveJoystick;

    private PlayerInput _input;
    private PlayerData _playerData;
    private bool _isBoosting;
    private float _boostTimeRemaining;

    public void Construct(PlayerData playerData, PlayerInput playerInput)
    {
        _playerData = playerData ?? throw new System.ArgumentNullException(nameof(playerData));
        _input = playerInput ?? throw new System.ArgumentNullException(nameof(playerInput));
        _input.Enable();
    }

    private void Update()
    {
        Vector2 moveDirection = _input.Player.Move.ReadValue<Vector2>();
        Move(moveDirection);
    }

    private void OnDisable()
    {
        _input.Disable();
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
}

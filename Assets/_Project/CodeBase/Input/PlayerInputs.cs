using System;
using UnityEngine;

public class PlayerInputs
{
    private PlayerInput _playerInput;

    public bool JumpTriggered => _playerInput.Player.Jump.triggered;
    public Vector2 MoveDirection => _playerInput.Player.Move.ReadValue<Vector2>();

    public PlayerInputs(PlayerInput playerInput)
    {
        _playerInput = playerInput ?? throw new ArgumentNullException(nameof(playerInput));
    }

    public void EnableInput() => _playerInput.Enable();

    public void DisableInput() => _playerInput.Disable();
}
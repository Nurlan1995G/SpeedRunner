using Assets._Project.Config;
using UnityEngine;

public class PlayerJumper : MonoBehaviour
{
    private PlayerMover _playerMover;
    private Player _player;
    private CharacterData _playerData;

    public void Construct(Player player, CharacterData playerData, PlayerMover playerMover)
    {
        _player = player;
        _playerData = playerData;
        _playerMover = playerMover;
    }

    private void Update()
    {
        _player.CharacterController.Move(Vector3.zero);
        HandleJump();
    }

    private void HandleJump()
    {
        if (_player.GroundChecker.IsGrounded && _player.PlayerInput.Player.Jump.triggered)
            _playerMover.TakeJumpDirection(_playerData.HeightJump);
        else if (_player.GroundChecker.IsOnTrampoline) 
            _playerMover.TakeJumpDirection(_playerData.HeightJump * 1.5f);
    }
}

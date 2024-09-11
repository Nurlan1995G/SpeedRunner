using Assets._Project.Config;
using UnityEngine;

public class PlayerJumper : MonoBehaviour
{
    private PlayerMover _playerMover;
    private Player _player;
    private PlayerData _playerData;

    public void Construct(Player player, PlayerMover playerMover)
    {
        _player = player;
        _playerData = _player.CharacterData;
        _playerMover = playerMover;
    }

    private void Update()
    {
        _player.CharacterController.Move(Vector3.zero);
        HandleJump();
    }

    public void TrampolineJumpUp() => 
        _playerMover.TakeJumpDirection(_playerData.HeightJump * _playerData.JumpStep);

    private void HandleJump()
    {
        if (_player.GroundChecker.IsGrounded && _player.PlayerInputs.JumpTriggered)
            _playerMover.TakeJumpDirection(_playerData.HeightJump);
    }
}

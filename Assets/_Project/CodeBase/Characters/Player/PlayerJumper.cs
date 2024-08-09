using Assets._Project.Config;
using UnityEngine;

public class PlayerJumper : MonoBehaviour
{
    private PlayerMover _playerMover;
    private Player _player;
    private CharacterData _playerData;

    private LayerMask _layerMaskGround;
    private LayerMask _layerMaskTrampoline;

    private const string GroundMask = "Ground";
    private const string TrampolineMask = "Trampoline";

    public void Construct(Player player, CharacterData playerData, PlayerMover playerMover)
    {
        _player = player;
        _playerData = playerData;
        _playerMover = playerMover;

        SetMask();
    }

    private void Update()
    {
        _player.CharacterController.Move(Vector3.zero);
        HandleJump();
    }

    private void SetMask()
    {
        _layerMaskGround = LayerMask.GetMask(GroundMask);
        _layerMaskTrampoline = LayerMask.GetMask(TrampolineMask);
    }

    private bool IsGrounded() => 
        CheckLayerCollision(_layerMaskGround);

    private bool CheckLayerCollision(LayerMask layerMask)
    {
        return Physics.Raycast(transform.position, Vector3.down, 
            _player.CharacterController.height / 2 + 0.1f, layerMask);
    }

    private void HandleJump()
    {
        if (IsGrounded() && _player.PlayerInput.Player.Jump.triggered)
            _playerMover.TakeJumpDirection(_playerData.HeightJump);
        else if(CheckLayerCollision(_layerMaskTrampoline))
            _playerMover.TakeJumpDirection(_playerData.HeightJump * 1.5f);
    }
}

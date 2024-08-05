using Assets._Project.Config;
using UnityEngine;

public class PlayerJumper : MonoBehaviour
{
    private CharacterController _characterController;
    private PlayerMover _playerMover;
    private PlayerInput _playerInput;
    private CharacterData _playerData;

    private LayerMask _layerMaskGround;
    private LayerMask _layerMaskTrampoline;

    private Vector3 _velocity;
    private float _jumpVelocity;

    private const string GroundMask = "Ground";
    private const string TrampolineMask = "Trampoline";

    public void Construct(CharacterData playerData, CharacterController characterController, PlayerInput playerInput, PlayerMover playerMover)
    {
        _playerData = playerData;
        _playerInput = playerInput;
        _characterController = characterController;
        _playerMover = playerMover;

        _playerInput.Enable();
        
        SetMask();
    }

    private void Update()
    {
        _characterController.Move(Vector3.zero);
        HandleJump();
    }

    private void OnDisable() => 
        _playerInput.Disable();

    private void SetMask()
    {
        _layerMaskGround = LayerMask.GetMask(GroundMask);
        _layerMaskTrampoline = LayerMask.GetMask(TrampolineMask);
    }

    private bool IsGrounded() => 
        CheckLayerCollision(_layerMaskGround);

    private bool CheckLayerCollision(LayerMask layerMask)
    {

        return Physics.Raycast(transform.position, Vector3.down, _characterController.height / 2 + 0.1f,
            layerMask);
    }

    private void HandleJump()
    {
        if (IsGrounded() && _playerInput.Player.Jump.triggered)
            _playerMover.TakeJumpDirection(_playerData.HeightJump);
        else if(CheckLayerCollision(_layerMaskTrampoline))
            _playerMover.TakeJumpDirection(_playerData.HeightJump * 1.5f);
    }
}
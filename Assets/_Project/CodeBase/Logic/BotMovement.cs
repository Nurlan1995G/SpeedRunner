using UnityEngine;

public class BotMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _jumpTrampoline = 10f;

    private BotController _botController;
    private Vector3 _velocity;

    public void Construct(BotController botController)
    {
        _botController = botController;
    }

    public void Move(Vector3 direction)
    {
        Vector3 move = direction * _moveSpeed * Time.deltaTime;
        MoveCharacterController(move + _velocity * Time.deltaTime);
    }

    public void Jump()
    {
        if(_botController.GroundChecker.IsOnJumpBot)
            _velocity.y = _jumpForce;
        else if(_botController.GroundChecker.IsOnTrampoline)
            _velocity.y = _jumpTrampoline;

        MoveCharacterController(_velocity * Time.deltaTime);
    }

    public void ApplyGravity(float jumpGravity) => 
        _velocity.y -= jumpGravity * Time.deltaTime;

    public void ResetVerticalVelocity() => 
        _velocity.y = 0;

    private void MoveCharacterController(Vector3 direction) => 
        _botController.CharacterController.Move(direction);
}

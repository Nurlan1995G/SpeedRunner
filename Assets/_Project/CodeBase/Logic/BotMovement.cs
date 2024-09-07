using UnityEngine;

public class BotMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _jumpTrampoline = 10f;
    [SerializeField] private float _boostUp = 100f;

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
        if (_botController.GroundChecker.IsOnJumpBot)
            _velocity.y = _jumpForce;
        else if (_botController.GroundChecker.IsOnTrampoline)
            _velocity.y = _jumpTrampoline;
        else if (_botController.GroundChecker.IsOnBoostUp)
            _velocity.y = _boostUp;

        MoveCharacterController(_velocity * Time.deltaTime);
    }

    public void ApplyGravity(float jumpGravity)
    {
        _velocity.y -= jumpGravity * Time.deltaTime;
    }

    public void ResetVerticalVelocity() => 
        _velocity.y = 0;

    private void MoveCharacterController(Vector3 direction) => 
        _botController.CharacterController.Move(direction);

    public void Rotate(Vector3 direction, float rotateSpeed)
    {
        if (direction != Vector3.zero) 
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            Quaternion restrictedRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, restrictedRotation, rotateSpeed * Time.deltaTime);
        }
    }
}

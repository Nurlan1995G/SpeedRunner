using UnityEngine;

public class BotMovement : MonoBehaviour
{
    private BotController _botController;

    private Vector3 _velocity;

    public float MovementSpeed { get; private set; }
    public Vector3 Velocity => _velocity;

    public void Construct(BotController botController)
    {
        _botController = botController;
    }

    public void Move(Vector3 direction)
    {
        Vector3 move = direction * RandomMove() * Time.deltaTime;
        MovementSpeed = move.magnitude;
        MoveCharacterController(move + _velocity * Time.deltaTime);
    }

    public void Rotate(Vector3 direction, float rotateSpeed)
    {
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            Quaternion restrictedRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, restrictedRotation, rotateSpeed * Time.deltaTime);
        }
    }

    public void Jump()
    {
        if (_botController.GroundChecker.IsOnJumpBot)
            _velocity.y = _botController.BotControllerData.JumpForce;
        else if (_botController.GroundChecker.IsOnTrampoline)
            _velocity.y = _botController.BotControllerData.JumpTrampoline;
        else if (_botController.GroundChecker.IsOnBoostUp)
            _velocity.y = _botController.BotControllerData.BoostUp;

        MoveCharacterController(_velocity * Time.deltaTime);
    }

    public void ApplyGravity(float jumpGravity, float maxGravitySpeed)
    {
        _velocity.y -= jumpGravity * Time.deltaTime;
        _velocity.y = Mathf.Max(_velocity.y, -maxGravitySpeed);
    }

    public void ResetVerticalVelocity() =>
        _velocity.y = 0;

    private void MoveCharacterController(Vector3 direction) => 
        _botController.CharacterController.Move(direction);

    private float RandomMove() => Random.Range(_botController.BotControllerData.MinMoveSpeed, _botController.BotControllerData.MaxMoveSpeed);
}

using UnityEngine;

public class BotMovement : MonoBehaviour
{
    private BotController _botController;
    private Vector3 _velocity;

    public float MovementSpeed { get; private set; }
    public Vector3 Velocity => _velocity;

    public void Construct(BotController botController) =>
        _botController = botController;

    public void Move(Vector3 direction, float currentSpeed)
    {
        if (_botController.IsClimbing)
        {
            Vector3 horizontal = new Vector3(direction.x, 0, 0);
            Vector3 climbMove = horizontal * currentSpeed * Time.deltaTime;
            MoveCharacterController(climbMove * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            Vector3 move = direction * currentSpeed * Time.deltaTime;
            MovementSpeed = move.magnitude;
            MoveCharacterController(move + _velocity * Time.deltaTime);
        }
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

    public void Jump(float jumpValue)
    {
        _velocity.y = jumpValue;

        MoveCharacterController(_velocity * Time.deltaTime);
    }

    public void ApplyGravity(float jumpGravity, float maxGravitySpeed)
    {
        if (_botController.IsClimbing == false)
        {
            _velocity.y -= jumpGravity * Time.deltaTime;
            _velocity.y = Mathf.Max(_velocity.y, -maxGravitySpeed);
        }

        //_botController.CharacterController.Move(_velocity * Time.deltaTime);
    }

    public void SetVelocityZero() =>
        _velocity = Vector3.zero;

    private void MoveCharacterController(Vector3 direction) =>
        _botController.CharacterController.Move(direction);
}

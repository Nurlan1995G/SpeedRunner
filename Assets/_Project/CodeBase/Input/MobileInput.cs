using UnityEngine;

public class MobileInput : IInput
{
    private VariableJoystick _joystick;
    private VariableJoystick _cameraJoystick;
    private JumpButtonHeandler _jumpButton;

    public Vector2 MouseDirection => GiveDirection(DirectionalFeature.MouseDirection);
    public Vector3 CharacterDirection => GiveDirection(DirectionalFeature.CharacterDirection);
    public Vector3 DirectionMovementOnLadder => GiveDirection(DirectionalFeature.DirectionMovementOnLadder);
    public bool JumpKeyDown => _jumpButton.IsPointerDown;
    public bool JumpKeyUp => _jumpButton.IsPointerUp;
    public bool IsMobileInput => true;
    public float JoystickMagnitude => _joystick.Direction.magnitude;
    public bool IsActivateInput { get; private set; }

    public MobileInput(VariableJoystick joystick, JumpButtonHeandler jumpButton, VariableJoystick cameraJoystick)
    {
        _joystick = joystick;
        _jumpButton = jumpButton;
        _cameraJoystick = cameraJoystick;
        IsActivateInput = true;
    }


    public void DisablingJumpKey()
    {
        _jumpButton.PointerDownDisabling();
    }

    public void ActivateDirection()
    {
        IsActivateInput = true;
    }

    public void DisablingDirection()
    {
        IsActivateInput = false;
    }

    private Vector3 GiveDirection(DirectionalFeature directionalFeature)
    {
        if (IsActivateInput)
        {
            if (directionalFeature == DirectionalFeature.MouseDirection)
                return new Vector3(_cameraJoystick.Horizontal, _cameraJoystick.Vertical, 0);
            else if (directionalFeature == DirectionalFeature.CharacterDirection)
                return new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);
            else
                return new Vector3(0, _joystick.Vertical, 0);
        }
        else
        {
            return Vector3.zero;
        }
    }
}
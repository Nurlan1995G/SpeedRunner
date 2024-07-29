using System;
using UnityEngine;

public class KeyboardInput : IInput
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private Joystick _cameraJoystick;

    public Vector2 MouseDirection => GiveDirection(DirectionalFeature.MouseDirection);
    public Vector3 CharacterDirection => GiveDirection(DirectionalFeature.CharacterDirection);
    public Vector3 DirectionMovementOnLadder => GiveDirection(DirectionalFeature.DirectionMovementOnLadder);
    public bool JumpKeyDown => Input.GetKeyDown(KeyCode.Space);
    public bool JumpKeyUp => Input.GetKeyUp(KeyCode.Space);
    public bool IsMobileInput => false;
    public bool IsActivateInput { get; private set; }

    public KeyboardInput(Joystick cameraJoystick)
    {
        _cameraJoystick = cameraJoystick;
        IsActivateInput = true;
    }

    public void DisablingJumpKey()
    {
        Input.GetKeyUp(KeyCode.Space);
    }

    public void DisablingDirection()
    {
        IsActivateInput = false;
    }

    public void ActivateDirection()
    {
        IsActivateInput = true;
    }

    private Vector3 GiveDirection(DirectionalFeature directionalFeature)
    {
        if (IsActivateInput)
        {
            if (directionalFeature == DirectionalFeature.MouseDirection)
                return new Vector3(_cameraJoystick.Horizontal, _cameraJoystick.Vertical, 0);
            else if (directionalFeature == DirectionalFeature.CharacterDirection)
                return new Vector3(Input.GetAxis(HORIZONTAL), 0, Input.GetAxis(VERTICAL));
            else
                return new Vector3(0, Input.GetAxis(VERTICAL), 0);
        }
        else
        {
            return Vector3.zero;
        }
    }
}

public enum DirectionalFeature
{
    CharacterDirection,
    DirectionMovementOnLadder,
    MouseDirection
}
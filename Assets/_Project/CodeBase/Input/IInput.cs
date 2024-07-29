using System;
using UnityEngine;

public interface IInput
{
    public Vector3 CharacterDirection { get; }
    public Vector3 DirectionMovementOnLadder { get; }
    public Vector2 MouseDirection { get; }

    public bool JumpKeyDown { get; }
    public bool JumpKeyUp { get; }
    public bool IsMobileInput { get; }
    public bool IsActivateInput { get; }

    void DisablingJumpKey();
    void DisablingDirection();
    void ActivateDirection();
}
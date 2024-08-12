using UnityEngine;

public class CharacterAnimation
{
    private const string IsIdling = "IsIdling";
    private const string IsRunning = "IsRunning";
    private const string IsJumping = "IsJumping";
    private const string IsFalling = "IsFalling";

    private Animator _animator;

    public CharacterAnimation(Animator animator)
    {
        _animator = animator;
    }

    public void StartIdle() => _animator.SetBool(IsIdling, true);
    public void StopIdle() => _animator.SetBool(IsIdling, false);

    public void StartRunning() => _animator.SetBool(IsRunning, true);
    public void StopRunning() => _animator.SetBool(IsRunning, false);

    public void StartJumping() => _animator.SetBool(IsJumping, true);
    public void StopJumping() => _animator.SetBool(IsJumping, false);

    public void StartFalling() => _animator.SetBool(IsFalling, true);
    public void StopFalling() => _animator.SetBool(IsFalling, false);
}
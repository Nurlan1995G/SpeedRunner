using UnityEngine;

public class PlayerAnimation
{
    private const string IsIdling = "IsIdling";
    private const string IsRunning = "IsRunning";
    private const string IsJumping = "IsJumping";
    private const string IsFalling = "IsFalling";
    private const string IsClimbing = "IsClimbing";

    private SkinHandler _skinHandler;
    private Player _player;

    public PlayerAnimation(SkinHandler skinHandler, Player player)
    {
        _skinHandler = skinHandler;
        _player = player;
    }

    public void StartIdle() => _skinHandler.CurrentSkin.Animator.SetBool(IsIdling, true);
    public void StopIdle() => _skinHandler.CurrentSkin.Animator.SetBool(IsIdling, false);

    public void StartRunning() => _skinHandler.CurrentSkin.Animator.SetBool(IsRunning, true);
    public void StopRunning() => _skinHandler.CurrentSkin.Animator.SetBool(IsRunning, false);

    public void StartJumping() => _skinHandler.CurrentSkin.Animator.SetBool(IsJumping, true);
    public void StopJumping() => _skinHandler.CurrentSkin.Animator.SetBool(IsJumping, false);

    public void StartFalling() => _skinHandler.CurrentSkin.Animator.SetBool(IsFalling, true);
    public void StopFalling() => _skinHandler.CurrentSkin.Animator.SetBool(IsFalling, false);

    public void StartClimb() => _skinHandler.CurrentSkin.Animator.SetBool(IsClimbing, true);
    public void StopClimb() => _skinHandler.CurrentSkin.Animator.SetBool(IsClimbing, false);

    public void HandleAnimations(Vector2 moveDirection, Vector3 velocityDirection, bool isClimbing)
    {
        if (_player.GroundChecker.IsGrounded)
        {
            StopFalling();
            StopJumping();

            if (moveDirection != Vector2.zero)
            {
                StartRunning();
                StopIdle();
            }
            else
            {
                StopRunning();
                StartIdle();
            }
        }
        else if (isClimbing)
        {
            StopJumping();
            StopFalling();
            StopRunning();
            StartClimb();
        }
        else
        {
            StopClimb();
            StopRunning();
            StopIdle();

            if (velocityDirection.y > 0)
            {
                StopIdle();
                StartJumping();
                StopFalling();
            }
            else
            {
                StopJumping();
                StartFalling();
            }
        }
    }
}
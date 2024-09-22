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

    private void StartIdle() => _skinHandler.CurrentSkin.Animator.SetBool(IsIdling, true);
    private void StopIdle() => _skinHandler.CurrentSkin.Animator.SetBool(IsIdling, false);

    private void StartRunning() => _skinHandler.CurrentSkin.Animator.SetBool(IsRunning, true);
    private void StopRunning() => _skinHandler.CurrentSkin.Animator.SetBool(IsRunning, false);

    public void StartJumping() => _skinHandler.CurrentSkin.Animator.SetBool(IsJumping, true);
    public void StopJumping() => _skinHandler.CurrentSkin.Animator.SetBool(IsJumping, false);

    private void StartFalling() => _skinHandler.CurrentSkin.Animator.SetBool(IsFalling, true);
    private void StopFalling() => _skinHandler.CurrentSkin.Animator.SetBool(IsFalling, false);

    private void StartClimb() => _skinHandler.CurrentSkin.Animator.SetBool(IsClimbing, true);
    private void StopClimb() => _skinHandler.CurrentSkin.Animator.SetBool(IsClimbing, false);

    public void HandleAnimations(Vector2 moveDirection, Vector3 velocityDirection, bool isDance, bool isClimbing)
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

            if (moveDirection != Vector2.zero)
            {
                StopIdle();
                StartClimb();
            }
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
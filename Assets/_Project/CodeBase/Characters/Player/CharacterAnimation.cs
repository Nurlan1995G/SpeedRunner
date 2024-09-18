using UnityEngine;

public class CharacterAnimation
{
    private const string IsIdling = "IsIdling";
    private const string IsRunning = "IsRunning";
    private const string IsJumping = "IsJumping";
    private const string IsFalling = "IsFalling";
    private const string IsDance = "IsDance";

    private SkinHandler _skinHandler;
    private Player _player;

    public CharacterAnimation(SkinHandler skinHandler, Player player)
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

    public void StartDance() => _skinHandler.CurrentSkin.Animator.SetBool(IsDance, true);
    public void StopDance() => _skinHandler.CurrentSkin.Animator.SetBool(IsDance, false);

    public void HandleAnimations(Vector2 moveDirection, Vector3 velocityDirection, bool isDance)
    {


        if (isDance == false)
        {
            StopDance();

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
            else
            {
                StopRunning();
                StopIdle();

                if (velocityDirection.y > 0)
                {
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
        else
        {
            StartDance();
            isDance = false;
        }
    }
}
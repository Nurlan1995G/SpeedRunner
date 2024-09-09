using UnityEngine;

public class CharacterAnimationBot
{
    private const string IsIdling = "IsIdling";
    private const string IsRunning = "IsRunning";
    private const string IsJumping = "IsJumping";
    private const string IsFalling = "IsFalling";

    private BotSkinHendler _skinHandler;
    private BotController _botController;
    private BotMovement _movement;

    public CharacterAnimationBot(BotSkinHendler skinHandler, BotController botController, BotMovement movement)
    {
        _skinHandler = skinHandler;
        _botController = botController;
        _movement = movement;
    }

    public void StartIdle() => _skinHandler.CurrentSkin.Animator.SetBool(IsIdling, true);
    public void StopIdle() => _skinHandler.CurrentSkin.Animator.SetBool(IsIdling, false);

    public void StartRunning() => _skinHandler.CurrentSkin.Animator.SetBool(IsRunning, true);
    public void StopRunning() => _skinHandler.CurrentSkin.Animator.SetBool(IsRunning, false);

    public void StartJumping() => _skinHandler.CurrentSkin.Animator.SetBool(IsJumping, true);
    public void StopJumping() => _skinHandler.CurrentSkin.Animator.SetBool(IsJumping, false);

    public void StartFalling() => _skinHandler.CurrentSkin.Animator.SetBool(IsFalling, true);
    public void StopFalling() => _skinHandler.CurrentSkin.Animator.SetBool(IsFalling, false);

    public void HandleAnimations(float moveDirection, Vector3 velocityDirection)
    {
        //Debug.Log("moveDirection - " + moveDirection);
        //Debug.Log("velocityDirection - " + velocityDirection);

        if (_botController.GroundChecker.IsGrounded)
        {
            StopFalling();
            StopJumping();

            if (moveDirection != 0)
            {
                //Debug.Log("Runn");
                StartRunning();
                StopIdle();
            }
            else
            {
                //Debug.Log("Idle");
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
                //Debug.Log("Jump");
                StartJumping();
                StopFalling();
            }
            else
            {
                //Debug.Log("Fall");
                StopJumping();
                StartFalling();
            }
        }
    }
}

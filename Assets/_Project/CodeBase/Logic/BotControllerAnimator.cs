using UnityEngine;

public class BotControllerAnimator
{
    private readonly Animator _animator;
    private readonly int _idleHash = Animator.StringToHash("Idle");
    private readonly int _runningHash = Animator.StringToHash("RunningBot");
    private readonly int _fallingHash = Animator.StringToHash("FallingBot");
    private readonly int _jumpingHash = Animator.StringToHash("JumpBot");

    private Vector3 _previousPosition;
    private BotController _bot;
    private BotMovement _botMovement;

    public BotControllerAnimator(Animator animator, BotController bot, BotMovement botMovement)
    {
        _animator = animator;
        _bot = bot;
        _botMovement = botMovement;
    }

    public void Update()
    {
        Vector3 currentPosition = _bot.transform.position;
        float verticalVelocity = _botMovement.Velocity.y;
        Vector3 horizontalSpeed = _botMovement.Movement;

        float speedMagnitude = horizontalSpeed.magnitude;

        if (_bot.GroundChecker.IsGrounded)
        {
            if (speedMagnitude < 0.1f)
            {
                PlayIdle();
            }
            else
            {
                PlayRun();
            }
        }
        else
        {
            if (verticalVelocity > 0)
            {
                PlayJump();
            }
            else if (verticalVelocity < -0.1f)
            {
                PlayFall();
            }
        }

        _previousPosition = currentPosition;
    }

    public void PlayIdle() =>
        Play(_idleHash);

    public void PlayJump() =>
        Play(_jumpingHash);

    public void PlayFall() =>
        Play(_fallingHash);

    public void PlayRun() =>
        Play(_runningHash);

    private void Play(int hash) =>
        _animator.Play(hash);
}

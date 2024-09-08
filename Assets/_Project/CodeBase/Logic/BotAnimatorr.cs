using UnityEngine;

public class BotAnimatorr
{
    private readonly Animator _animator;
    private readonly int _idleHash = Animator.StringToHash("Idle");
    private readonly int _runningHash = Animator.StringToHash("RunningBot");
    private readonly int _fallingHash = Animator.StringToHash("FallingBot");
    private readonly int _jumpingHash = Animator.StringToHash("JumpBot");

    private Vector3 _previousPosition;
    private BotController _bot;
    private BotMovement _botMovement;

    public BotAnimatorr(Animator animator, BotController bot, BotMovement botMovement)
    {
        _animator = animator;
        _bot = bot;
        _botMovement = botMovement;
    }

    public void Update()
    {
        Vector3 currentPosition = _bot.transform.position;
        float verticalVelocity = (_botMovement.Velocity.y);

        if (_bot.GroundChecker.IsGrounded)
        {
            if (currentPosition == _previousPosition)
                PlayIdle();
            else
                PlayRun();
        }
        else
        {
            if (verticalVelocity > 0)
                PlayJump();
            else if (verticalVelocity < 0)
                PlayFall();
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

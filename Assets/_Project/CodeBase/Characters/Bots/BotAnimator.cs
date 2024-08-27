using UnityEngine;

public class BotAnimator
{
    private readonly Animator _animator;
    private readonly int _idleHash = Animator.StringToHash("Idle");
    private readonly int _runningHash = Animator.StringToHash("RunningBot");
    private readonly int _fallingHash = Animator.StringToHash("FallingBot");

    private Vector3 _position;
    private BotView _bot;

    public BotAnimator(Animator animator, BotView bot)
    {
        _animator = animator;
        _bot = bot;
    }

    public void Update(bool IsActivateJetpack)
    {
        if (_bot.transform.position == _position)
        {
            PlayIdle();
        }
        else
        {
            if (IsActivateJetpack && _bot.GroundChecker.IsGrounded == false)
            {
                PlayJump();
            }
            else
            {
                PlayRun();
            }
        }

        _position = _bot.transform.position;
    }

    public void PlayIdle() =>
        Play(_idleHash);

    public void PlayJump() =>
        Play(_fallingHash);

    public void PlayRun() =>
        Play(_runningHash);

    private void Play(int hash) =>
        _animator.Play(hash);
}

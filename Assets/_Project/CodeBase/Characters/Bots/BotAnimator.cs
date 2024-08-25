using UnityEngine;

public class BotAnimator
{
    private readonly Animator _animator;
    private readonly int _idleHash = Animator.StringToHash("Idle");
    private readonly int _runningHash = Animator.StringToHash("RunningBot");
    private readonly int _fallingHash = Animator.StringToHash("FallingBot");

    private Vector3 _position;
    private Transform _transform;

    public BotAnimator(Animator animator, Transform transform)
    {
        _animator = animator;
        _transform = transform;
    }

    public void Update(bool IsActivateJetpack)
    {
        if (_transform.position == _position)
        {
            PlayIdle();
        }
        else
        {
            if (IsActivateJetpack)
            {
                PlayJump();
            }
            else
            {
                PlayRun();
            }
        }

        _position = _transform.position;
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

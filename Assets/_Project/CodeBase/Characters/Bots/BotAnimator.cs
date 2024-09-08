using UnityEngine;

public class BotAnimator
{
    private readonly Animator _animator;
    private readonly int _idleHash = Animator.StringToHash("Idle");
    private readonly int _runningHash = Animator.StringToHash("RunningBot");
    private readonly int _fallingHash = Animator.StringToHash("FallingBot");
    private readonly int _jumpingHash = Animator.StringToHash("JumpBot");

    private Vector3 _position;
    private BotView _bot;
    private bool _isJumping;
    private bool _isFalling;

    public BotAnimator(Animator animator, BotView bot)
    {
        _animator = animator;
        _bot = bot;
    }

    public void Update()
    {
        Vector3 currentPosition = _bot.transform.position;
        bool isGrounded = _bot.GroundChecker.IsGrounded;
        Vector3 velocity = (currentPosition - _position) / Time.deltaTime;

        if (!isGrounded && velocity.y > 0 && !_isJumping && !_isFalling)
        {
            _isJumping = true;
            _isFalling = false;
            PlayJump();
        }

        if (!isGrounded && velocity.y < 0 && !_isFalling)
        {
            _isJumping = false;
            _isFalling = true;
            PlayFall();
        }

        if (isGrounded)
        {
            if (_isFalling || _isJumping)
            {
                _isFalling = false;
                _isJumping = false;
                PlayRun(); 
            }
            else if (velocity.magnitude < 0.1f)
            {
                PlayIdle();
            }
            else
            {
                PlayRun();
            }
        }

        _position = currentPosition;
    }

    public void PlayIdle() =>
        Play(_idleHash);

    public void PlayFall() =>
        Play(_fallingHash);

    public void PlayRun() =>
        Play(_runningHash);

    public void PlayJump() =>
        Play(_jumpingHash);

    private void Play(int hash) =>
        _animator.Play(hash);
}

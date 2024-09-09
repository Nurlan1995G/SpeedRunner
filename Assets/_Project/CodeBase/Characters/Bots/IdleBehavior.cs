public class IdleBehavior : IBehaviour
{
    private BotAgentAnimator _botAnimator;
    private BotMover _botMover;

    public IdleBehavior(BotAgentAnimator botAnimator, BotMover botMover)
    {
        _botAnimator = botAnimator;
        _botMover = botMover;
    }

    public void Activate()
    {
        _botAnimator.PlayIdle();
        _botMover.StopMovement();
    }

    public void Deactivate()
    {
        _botMover.StopMovement();
    }
}

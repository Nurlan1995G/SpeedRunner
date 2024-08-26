public class IdleBehavior : IBehaviour
{
    private BotAnimator _botAnimator;
    private BotMover _botMover;

    public IdleBehavior(BotAnimator botAnimator, BotMover botMover)
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

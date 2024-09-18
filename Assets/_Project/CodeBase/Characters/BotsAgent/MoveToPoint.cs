public class MoveToPoint : IBehaviour
{
    private readonly BotMover _botMover;
    private readonly FlagPoint _targetPoint;
    private readonly BotAgentAnimator _botAnimator;

    public MoveToPoint(BotMover botMover, FlagPoint targetPoint, BotAgentAnimator botAnimator)
    {
        _botMover = botMover;
        _targetPoint = targetPoint;
        _botAnimator = botAnimator;
    }

    public void Activate()
    {
        _botAnimator.PlayRun();
        _botMover.MoveTo(_targetPoint);
    }

    public void Deactivate() =>
        _botMover.StopMovement();
}

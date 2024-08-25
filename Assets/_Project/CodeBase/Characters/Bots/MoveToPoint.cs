public class MoveToPoint
{
    private readonly BotMover _botMover;
    private readonly FlagPoint _targetPoint;
    private readonly BotAnimator _botAnimator;

    public MoveToPoint(BotMover botMover, FlagPoint targetPoint, BotAnimator botAnimator)
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

    public void Diactivate() =>
        _botMover.StopMovement();
}

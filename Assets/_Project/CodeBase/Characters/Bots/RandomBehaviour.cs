public class RandomBehaviour : IBehaviour
{
    private BotAnimator _botAnimator;
    private BotMover _botMover;

    public RandomBehaviour(BotAnimator botAnimator, BotMover botMover)
    {
        _botAnimator = botAnimator;
        _botMover = botMover;
    }

    public void Activate()
    {
        _botAnimator.PlayRun();
        _botMover.StartRandomMove();
    }

    public void Deactivate()
    {
        _botMover.StopMovement();
    }
}

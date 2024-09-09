public class RandomBehaviour : IBehaviour
{
    private BotAgentAnimator _botAnimator;
    private BotMover _botMover;

    public RandomBehaviour(BotAgentAnimator botAnimator, BotMover botMover)
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

public class BotMover
{
    private BotView _botView;

    public BotMover(BotView botView)
    {
        _botView = botView;

        _botView.Agent.speed = _botView.CharacterBotData.MoveSpeed;
    }

    public void MoveTo(FlagPoint flagPoint)
    {
         _botView.Agent.isStopped = false;
         _botView.Agent.destination = flagPoint.transform.position;
    }

    public void StopMovement()
    {
         _botView.Agent.isStopped = true;
         _botView.Agent.SetDestination(_botView.transform.position);
    }
}

using UnityEngine.AI;

public class BotMover
{
    private NavMeshAgent _agent;
    private BotView _botView;

    public BotMover(NavMeshAgent agent, BotView botView)
    {
        _agent = agent;
        _botView = botView;

        _agent.speed = _botView.CharacterBotData.MoveSpeed;
    }

    public void MoveToPoint(FlagPoint flagPoint)
    {
        _agent.isStopped = false;
        _agent.destination = flagPoint.transform.position;
    }

    public void StopMovement()
    {
        _agent.isStopped = true;
        _agent.SetDestination(_botView.transform.position);
    }
}

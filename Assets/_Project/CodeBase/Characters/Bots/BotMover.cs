using UnityEngine.AI;
using UnityEngine;
using System.Collections;

public class BotMover
{
    private BotView _botView;

    private Coroutine _coroutine;

    public BotMover(BotView botView)
    {
        _botView = botView;

        _botView.Agent.speed = _botView.CharacterBotData.BotMoveSpeed;
    }

    public void MoveTo(FlagPoint flagPoint)
    {
         _botView.Agent.isStopped = false;
         _botView.Agent.destination = flagPoint.transform.position;
    }

    public void StopMovement()
    {
        if (_coroutine != null)
            CoroutineRunner.Instance.Stop(_coroutine);

         _botView.Agent.isStopped = true;
         _botView.Agent.SetDestination(_botView.transform.position);
    }

    public void StartRandomMove() =>
        _coroutine = CoroutineRunner.Instance.RunCoroutine(RandomMoving());

    private IEnumerator RandomMoving()
    {
        _botView.Agent.isStopped = false;

        var waitForSeconds = new WaitForSeconds(1f);

        while (true)
        {
            if (_botView.Agent.remainingDistance <= _botView.Agent.stoppingDistance)
            {
                if (IsRandomPointFound(_botView.transform.position, _botView.CharacterBotData.BotRangeRandomMoving, out Vector3 point))
                {
                    _botView.Agent.SetDestination(point);
                    _botView.transform.LookAt(point);
                }
            }

            yield return waitForSeconds;
        }
    }

    private bool IsRandomPointFound(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;

        if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 20.0f, NavMesh.AllAreas))
        {
            result = hit.position;

            return true;
        }

        result = Vector3.zero;

        return false;
    }
}

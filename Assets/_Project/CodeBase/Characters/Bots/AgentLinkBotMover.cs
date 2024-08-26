using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AgentLinkBotMover : MonoBehaviour
{
    [SerializeField] private BotView _bot;

    private bool _isRespawned;

    private void OnEnable()
    {
        _bot.Respawned += OnRespawned;
    }

    private void OnDisable()
    {
        _bot.Respawned -= OnRespawned;
    }

    private IEnumerator Start()
    {
        _bot.Agent.autoTraverseOffMeshLink = false;

        while (true)
        {
            if (_bot.Agent.isOnOffMeshLink)
            {
                yield return new WaitWhile(() => _bot.Agent.velocity != Vector3.zero);

                 yield return StartCoroutine(Parabola(_bot.Agent, _bot.CharacterBotData.HeightJump, _bot.CharacterBotData.JumpDuration));
            }

            yield return null;
        }
    }

    private IEnumerator WaitingForRespawn()
    {
        yield return new WaitUntil(() => _isRespawned);
        yield return new WaitForSeconds(0.1f);
        _bot.ChagePosition();

    }

    private void OnRespawned()
    {
        _isRespawned = true;

        StartCoroutine(WaitingForRespawn());
    }

    private IEnumerator NormalSpeed(NavMeshAgent agent)
    {
        OffMeshLinkData data = agent.currentOffMeshLinkData;
        Vector3 endPos = data.endPos + Vector3.up * agent.baseOffset;
        while (agent.transform.position != endPos)
        {
            agent.transform.position = Vector3.MoveTowards(agent.transform.position, endPos, agent.speed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator Parabola(NavMeshAgent agent, float height, float duration)
    {
        OffMeshLinkData data = agent.currentOffMeshLinkData;
        Vector3 startPos = agent.transform.position;
        Vector3 endPos = data.endPos + Vector3.up * agent.baseOffset;
        float normalizedTime = 0.0f;

        while (normalizedTime < 1.0f)
        {
            if (_isRespawned)
            {
                _isRespawned = false;

                yield break;
            }

            float yOffset = height * (normalizedTime - normalizedTime * normalizedTime);
            agent.transform.position = Vector3.Lerp(startPos, endPos, normalizedTime) + yOffset * Vector3.up;
            normalizedTime += Time.deltaTime / duration;

            yield return null;
        }

        _bot.Agent.CompleteOffMeshLink();
    }
}

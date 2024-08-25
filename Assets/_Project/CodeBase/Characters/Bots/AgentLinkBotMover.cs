using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentLinkBotMover : MonoBehaviour
{
    [SerializeField] private List<FlagPoint> _flagPoints;   
    [SerializeField] private BotView _bot;                  
    [SerializeField] private float _jumpCooldown = 0.1f;    

    private int _currentFlagIndex = 0;                     

    private void Start()
    {
        _bot.Agent.speed = _bot.CharacterBotData.MoveSpeed;
        MoveToNextFlag();  
    }

    private void Update()
    {
        if (_bot.Agent.isOnOffMeshLink && _bot.GroundChecker.IsGrounded)
        {
            StartCoroutine(JumpAcrossGap());  
        }
    }

    private IEnumerator JumpAcrossGap()
    {
        OffMeshLinkData data = _bot.Agent.currentOffMeshLinkData; 
        Vector3 startPos = _bot.Agent.transform.position;          
        Vector3 endPos = data.endPos + Vector3.up * _bot.Agent.baseOffset; 

        float normalizedTime = 0.0f;

        while (normalizedTime < _bot.CharacterBotData.NormalizedJumpTimeMax)
        {
            float yOffset = _bot.CharacterBotData.HeightJump * (normalizedTime - normalizedTime * normalizedTime);
            _bot.Agent.transform.position = Vector3.Lerp(startPos, endPos, normalizedTime) + yOffset * Vector3.up;
            normalizedTime += Time.deltaTime / _bot.CharacterBotData.JumpDuration;
            yield return null;
        }

        _bot.Agent.CompleteOffMeshLink();  

        yield return new WaitForSeconds(_jumpCooldown);

        MoveToNextFlag();  
    }

    private void MoveToNextFlag()
    {
        if (_flagPoints.Count == 0) return;  

        _currentFlagIndex = (_currentFlagIndex + 1) % _flagPoints.Count; 
        _bot.Agent.SetDestination(_flagPoints[_currentFlagIndex].transform.position);  
    }
}

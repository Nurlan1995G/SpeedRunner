using Assets._Project.CodeBase.Characters.Interface;
using Assets._Project.Config;
using UnityEngine;
using UnityEngine.AI;

public class BotView : MonoBehaviour, IRespawned
{   
    //[SerializeField] private FlagPoint _targetPoint;

    private BotMover _botMover;
    private Player _player;
    [field: SerializeField] public NavMeshAgent Agent;

    public CharacterData CharacterBotData { get; private set; }
    [field: SerializeField] public GroundChecker GroundChecker { get; private set; }
    [field: SerializeField] public BotNickName Nickname { get; private set; }

    public void Construct(CharacterData character)
    {
        CharacterBotData = character;

        _botMover = new(Agent, this);
    }

    public void Respawn()
    {
        Debug.Log("RespawnBots");
    }
}

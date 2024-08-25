using Assets._Project.CodeBase.Characters.Interface;
using Assets._Project.Config;
using UnityEngine;
using UnityEngine.AI;

public class BotView : MonoBehaviour, IRespawned
{
    [SerializeField] private Animator _botAnimator;

    private BotMover _botMover;
    private Player _player;

    [field: SerializeField] public GroundChecker GroundChecker { get; private set; }
    [field: SerializeField] public NavMeshAgent Agent { get; private set; }
    [field: SerializeField] public BotNickName Nickname { get; private set; }
    public CharacterAnimation CharacterAnimation { get; private set; }
    public CharacterData CharacterBotData { get; private set; }

    public void Construct(CharacterData character)
    {
        CharacterBotData = character;

        CharacterAnimation characterAnimation = new(_botAnimator);
        CharacterAnimation = characterAnimation;

        _botMover = new(Agent, this);
    }

    public void Respawn()
    {
        Debug.Log("RespawnBots");
    }
}

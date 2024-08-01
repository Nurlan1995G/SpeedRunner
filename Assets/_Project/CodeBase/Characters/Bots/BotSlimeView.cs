using Assets._Project.Config;
using Assets.Project.CodeBase.SharkEnemy;
using Assets.Project.CodeBase.SharkEnemy.StateMashine;
using UnityEngine;
using UnityEngine.AI;

public class BotSlimeView : MonoBehaviour   
{   
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private CharacterModel _sharkModel;

    private CharacterBotData _characterBotData;
    private PlayerView _player;
    private SlimeBotStateMachine _stateMashine;

    private void Start() =>
        _stateMashine = new SlimeBotStateMachine(_agent, _sharkModel, _characterBotData);

    private void Update() =>
        _stateMashine?.Update();

    public void Construct( CharacterBotData character, TopCharacterManager topCharacterManager)
    {
        _characterBotData = character;
        _sharkModel.Init(topCharacterManager);
    }

    public void SetNickname(string nickName) =>
        _sharkModel.SetNickName(nickName);
}

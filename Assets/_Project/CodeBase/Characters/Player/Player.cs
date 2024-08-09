using Assets._Project.Config;
using Assets.ProjectLesson2.Scripts.Character;
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private PlayerJumper _playerJumper;

    private PositionStaticData _positionStaticData;
    private SoundHandler _soundhandler;
    private Language _language;

    public Action RespawnedCheckpoints;
    public Action RespawnedFuelCanister;

    [field: SerializeField] public CharacterAnimation CharacterAnimation { get; private set; }
    [field: SerializeField] public CharacterController CharacterController { get; private set; }
    public PlayerInput PlayerInput { get; private set; }
    public Vector3 RespawnPosition { get; private set; }

    public void Construct(PositionStaticData positionStaticData, CharacterData playerData,
         SoundHandler soundHandler, Language language, PlayerInput playerInput, CharacterAnimation characterAnimation)
    {
        PlayerInput = playerInput;
        _positionStaticData = positionStaticData ?? throw new ArgumentNullException(nameof(positionStaticData));
        _soundhandler = soundHandler ?? throw new ArgumentNullException(nameof(soundHandler));
        _language = language;
        CharacterAnimation = characterAnimation;

        _playerMover.Construct(playerData, this);
        _playerJumper.Construct(this, playerData, _playerMover);
    }

    public void TryStart(bool isStartMoving)
    {
        if (isStartMoving)
        {
            TryEnableCharacter(true);
        }
        else
        {
            TryEnableCharacter(false);
        }
    }

    private void TryEnableCharacter(bool isEnable)
    {
        CharacterController.enabled = isEnable;
        _playerMover.enabled = isEnable;
    }
    
    public void Destroyable()
    {
        //PlayerDied?.Invoke(this);
        _soundhandler.PlayLose();
        gameObject.SetActive(false);

        Teleport();
    }

    private void Teleport()
    {
        transform.position = _positionStaticData.InitPlayerPosition;
        gameObject.SetActive(true);
        _soundhandler.PlayWin();
    }
}

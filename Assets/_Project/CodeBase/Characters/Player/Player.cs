using Assets._Project.Config;
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private PlayerJumper _playerJumper;
    [SerializeField] private CharacterController _characterController;

    private PositionStaticData _positionStaticData;
    private SoundHandler _soundhandler;
    private Language _language;

    public bool IsOnChekpoint;

    public Action RespawnedCheckpoints;
    public Action RespawnedFuelCanister;

    public Vector3 RespawnPosition { get; private set; }

    public void Construct(PositionStaticData positionStaticData, CharacterData playerData,
         SoundHandler soundHandler, Language language, PlayerInput playerInput)
    {
        _positionStaticData = positionStaticData ?? throw new ArgumentNullException(nameof(positionStaticData));
        _soundhandler = soundHandler ?? throw new ArgumentNullException(nameof(soundHandler));
        _language = language;

        _playerMover.Construct(playerData, playerInput, _characterController);
        _playerJumper.Construct(playerData, _characterController, playerInput, _playerMover);
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
        _characterController.enabled = isEnable;
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

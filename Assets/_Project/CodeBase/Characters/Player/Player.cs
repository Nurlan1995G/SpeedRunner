using Assets._Project.Config;
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private PlayerJumper _playerJumper;
    [SerializeField] private CharacterController _characterController;

    private PositionStaticData _positionStaticData;
    private CharacterData _playerData;
    private SoundHandler _soundhandler;

    public Action<Player> PlayerDied;
    private Language _language;

    public void Construct(PositionStaticData positionStaticData, CharacterData playerData,
         SoundHandler soundHandler, Language language)
    {
        _positionStaticData = positionStaticData ?? throw new ArgumentNullException(nameof(positionStaticData));
        _playerData = playerData;
        _soundhandler = soundHandler ?? throw new ArgumentNullException(nameof(soundHandler));
        _language = language;

        _playerJumper.Construct(_playerData, _characterController);
    }

    public void TryStart(bool isStartMoving)
    {
        if (isStartMoving)
        {

        }
        else
        {

        }
    }

    public void Destroyable()
    {
        PlayerDied?.Invoke(this);
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

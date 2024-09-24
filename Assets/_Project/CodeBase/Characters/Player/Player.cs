using Assets._Project.CodeBase.Characters.Interface;
using Assets._Project.Config;
using Cinemachine;
using System;
using UnityEngine;

public class Player : MonoBehaviour, IRespawned
{
    [SerializeField] private ParticleSystem _effectSpawnPlayer;
    [SerializeField] private CinemachineFreeLook _cinemachine;
    
    private SoundHandler _soundhandler;
    private PositionStaticData _positionStaticData;
    private PlayerMover _playerMover;
    private Vector3 _respawnPosition;
    private int _score = 0;

    [field: SerializeField] public CharacterController CharacterController { get; private set; }
    [field: SerializeField] public GroundChecker GroundChecker { get; private set; }
    public PlayerAnimation CharacterAnimation { get; private set; }
    public PlayerInputs PlayerInputs { get; private set; }
    public PlayerData CharacterData { get; private set; }
    public int Score => _score;
    
    private Action<Player> PlayerDied;

    public void Construct(PositionStaticData positionStaticData, PlayerData characterData,
         SoundHandler soundHandler,PlayerInputs playerInputs, PlayerMover playerMover, PlayerJumper playerJumper, SkinHandler skinHandler, PlayerAnimation characterAnimation)
    {
        PlayerInputs = playerInputs ?? throw new ArgumentNullException(nameof(playerInputs));
        _soundhandler = soundHandler ?? throw new ArgumentNullException(nameof(soundHandler));
        CharacterData = characterData;
        _positionStaticData = positionStaticData;
        _playerMover = playerMover;
        CharacterAnimation = characterAnimation;

        _playerMover.Construct(this);
        playerJumper.Construct(this, playerMover);
        RespawnPosition(_positionStaticData.InitPlayerPosition);
    }

    private void OnEnable() =>
        PlayerInputs.EnableInput();

    private void OnDisable() =>
        PlayerInputs.DisableInput();

    public void ActivateForRace()
    {
        RespawnPosition(_positionStaticData.InitPlayerPosition);
        Respawn();
        _playerMover.ResetStartSpeed();
    }

    public void Respawn()
    {
        gameObject.SetActive(false);
        transform.position = _respawnPosition;
        gameObject.SetActive(true);

        AdjustCameraPosition();

        _effectSpawnPlayer.Play();
        _soundhandler.PlayWin();
    }

    public void RespawnPosition(Vector3 position) =>
        _respawnPosition = position;

    public void SetScore(int score)
    {
        Wallet.Add(score);
        _score = score;
    }

    public void StopMovement() => 
        _playerMover.StopMovement();

    private void AdjustCameraPosition()
    {
        _cinemachine.gameObject.SetActive(false);

        Vector3 playerPosition = transform.position;

        Vector3 newCameraPosition = new Vector3(playerPosition.x + 50f, playerPosition.y, playerPosition.z); 

        _cinemachine.transform.position = newCameraPosition;

        _cinemachine.gameObject.SetActive(true);
    }
}

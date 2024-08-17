using Assets._Project.Config;
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private ParticleSystem _effectSpawnPlayer;

    private SoundHandler _soundhandler;
    private Language _language;

    private Vector3 _respawnPosition;
    private bool _respawn;
    private int _score = 0;

    private Action<Player> PlayerDied;

    [field: SerializeField] public CharacterController CharacterController { get; private set; }
    [field: SerializeField] public GroundChecker GroundChecker { get; private set; }
    public CharacterAnimation CharacterAnimation { get; private set; }
    public PlayerInputs PlayerInputs { get; private set; }
    public CharacterData CharacterData { get; private set; }
    public int Score => _score;

    public void Construct(PositionStaticData positionStaticData, CharacterData characterData,
         SoundHandler soundHandler, Language language, PlayerInputs playerInputs, CharacterAnimation characterAnimation, PlayerMover playerMover, PlayerJumper playerJumper)
    {
        PlayerInputs = playerInputs ?? throw new ArgumentNullException(nameof(playerInputs));
        _soundhandler = soundHandler ?? throw new ArgumentNullException(nameof(soundHandler));
        _language = language;
        CharacterData = characterData;
        CharacterAnimation = characterAnimation ?? throw new ArgumentNullException(nameof(characterAnimation));

        playerMover.Construct(this);
        playerJumper.Construct(this, playerMover);
        RespawnPosition(positionStaticData.InitPlayerPosition);
    }

    private void OnEnable() =>
        PlayerInputs.EnableInput();

    private void Update()
    {
        Destroyable();
    }

    private void OnDisable() =>
        PlayerInputs.DisableInput();

    public void Respawn(bool respawn) =>
        _respawn = respawn;

    public void RespawnPosition(Vector3 position) =>
        _respawnPosition = position;

    public void SetScore(int score) =>
        _score = score;

    private void Destroyable()
    {   
        //PlayerDied?.Invoke(this);

        if(transform.position.y < -20 || _respawn)
        {
            _soundhandler.PlayLose();
            gameObject.SetActive(false);
            Teleport();
        }
    }

    private void Teleport()
    {
        transform.position = _respawnPosition;
        gameObject.SetActive(true);
        _effectSpawnPlayer.Play();
        _respawn = false;
        _soundhandler.PlayWin();
    }
}

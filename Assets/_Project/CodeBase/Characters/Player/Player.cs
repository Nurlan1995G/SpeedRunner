using Assets._Project.Config;
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private PlayerJumper _playerJumper;
    [SerializeField] private ParticleSystem _effectSpawnPlayer;

    private PositionStaticData _positionStaticData;
    private SoundHandler _soundhandler;
    private Language _language;
    private bool _respawn;
    private int _score = 0;

    private Action<Player> PlayerDied;

    [field: SerializeField] public CharacterController CharacterController { get; private set; }
    [field: SerializeField] public GroundChecker GroundChecker { get; private set; }
    public CharacterAnimation CharacterAnimation { get; private set; }
    public PlayerInput PlayerInput { get; private set; }
    public GameConfig GameConfig { get; private set; }
    public int Score => _score;

    public void Construct(PositionStaticData positionStaticData, GameConfig gameConfig,
         SoundHandler soundHandler, Language language, PlayerInput playerInput, CharacterAnimation characterAnimation)
    {
        PlayerInput = playerInput ?? throw new ArgumentNullException(nameof(playerInput));
        _positionStaticData = positionStaticData ?? throw new ArgumentNullException(nameof(positionStaticData));
        _soundhandler = soundHandler ?? throw new ArgumentNullException(nameof(soundHandler));
        _language = language;
        GameConfig = gameConfig;
        CharacterAnimation = characterAnimation ?? throw new ArgumentNullException(nameof(characterAnimation));

        _playerMover.Construct(this);
        _playerJumper.Construct(this, _playerMover);
    }

    private void OnEnable() =>
        PlayerInput.Enable();

    private void Update()
    {
        Destroyable();
    }

    private void OnDisable() =>
        PlayerInput.Disable();
    
    public void Respawn(bool respawn)
    {
        _respawn = respawn;
    }

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
        transform.position = _positionStaticData.InitPlayerPosition;
        _effectSpawnPlayer.Play();
        gameObject.SetActive(true);
        _respawn = false;
        _soundhandler.PlayWin();
    }
}

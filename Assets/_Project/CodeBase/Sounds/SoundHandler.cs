using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    [SerializeField] private AudioSource _background;
    [SerializeField] private AudioSource _win;
    [SerializeField] private AudioSource _lose;
    [SerializeField] private AudioSource _buy;

    public static SoundHandler Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start() =>
        PlayBackground();

    public void PlayWin() =>
        _win.Play();

    public void PlayLose() =>
        _lose.Play();

    public void PlayBuy() =>
        _buy.Play();

    private void PlayBackground() =>
        _background.Play();
}

using UnityEngine;
using TMPro;

[RequireComponent(typeof(CanvasGroup))]
public class ADTimer : MonoBehaviour
{
    [SerializeField] private float _totalSecondToShowAd = 60;
    [SerializeField] private float _timeToShowDisplay = 5f;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private TMP_Text _label;

    private float _currentTime;
    private CanvasGroup _canvasGroup;
    private Canvas _canvas;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvas = GetComponent<Canvas>();
    }

    private void Start() => StartTimer();

    private void Update()
    {
        if (_currentTime > 0f)
        {
            _currentTime -= Time.unscaledDeltaTime;

            UpdateTimerText();

            if (_currentTime <= _timeToShowDisplay && _canvasGroup.alpha == 0)
            {
                _canvas.sortingOrder = 1000;
                Time.timeScale = 0;
                AudioListener.pause = true;
                _canvasGroup.alpha = 1f;

                if (YandexSDK.Instance != null)
                    YandexSDK.Instance.ChangeStateTimerVisible(true);
            }

            if (_currentTime <= 0f)
            {
                TimerExpired();
                StartTimer();
            }
        }
    }

    private void StartTimer()
    {
        _canvasGroup.alpha = 0f;
        _currentTime = _totalSecondToShowAd;
        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        if (_timerText != null)
        {
            int minutes = Mathf.FloorToInt(_currentTime / 60f);
            int seconds = Mathf.FloorToInt(_currentTime % 60f);

            _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    private void TimerExpired()
    {
        if (Application.isFocused)
        {
            _canvas.sortingOrder = -1;
            Time.timeScale = 1;
            AudioListener.pause = false;
        }

        if (YandexSDK.Instance != null)
            YandexSDK.Instance.ChangeStateTimerVisible(false);

#if UNITY_WEBGL && !UNITY_EDITOR
        YandexSDK.Instance.ShowInterstitial();  
#endif
    }
}
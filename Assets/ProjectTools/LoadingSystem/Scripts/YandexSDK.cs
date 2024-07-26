using System;
using UnityEngine;
using YG;

public class YandexSDK : MonoBehaviour
{
    public static YandexSDK Instance = null;

    private bool _isInterstitial;
    private bool _isReward;
    private bool _isTimer;

    public bool IsFocus { get; private set; }

    public SavesYG Data => YandexGame.savesData;

    public event Action ShowedRewarded;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        YandexGame.OpenFullAdEvent += OnOpenFullAdEvent;
        YandexGame.CloseFullAdEvent += OnCloseFullAdEvent;

        YandexGame.RewardVideoEvent += OnRewardVideoEvent;
        YandexGame.OpenVideoEvent += OnOpenVideoEvent;
        YandexGame.CloseVideoEvent += OnCloseVideoEvent;
        YandexGame.ErrorVideoEvent += OnErrorVideoEvent;
    }

    private void OnDisable()
    {
        YandexGame.OpenFullAdEvent -= OnOpenFullAdEvent;
        YandexGame.CloseFullAdEvent -= OnCloseFullAdEvent;
        YandexGame.ErrorFullAdEvent -= OnErrorFullAdEvent;

        YandexGame.RewardVideoEvent -= OnRewardVideoEvent;
        YandexGame.OpenVideoEvent -= OnOpenVideoEvent;
        YandexGame.CloseVideoEvent -= OnCloseVideoEvent;
        YandexGame.ErrorVideoEvent -= OnErrorVideoEvent;
    }

    #region VIDEO_AD
    public void ShowVideoAd(Action onRewardedCallback = null)
    {
        ShowedRewarded = onRewardedCallback;
        YandexGame.Instance._RewardedShow(0);
    }

    private void OnOpenVideoEvent() => _isReward = true;

    private void OnCloseVideoEvent() => _isReward = false;

    private void OnRewardVideoEvent(int id) => ShowedRewarded?.Invoke();

    private void OnErrorVideoEvent()
    {
        //Открываем дисплей ошибки
    }
    #endregion

    #region INTERSTITIAL
    public void ShowInterstitial() => YandexGame.Instance._FullscreenShow();

    private void OnOpenFullAdEvent() => _isInterstitial = true;

    private void OnCloseFullAdEvent() => _isInterstitial = false;

    private void OnErrorFullAdEvent()
    {
        MuteAudio(Data.IsMute);
        Time.timeScale = 1;
    }
    #endregion

    private void OnApplicationFocus(bool focus)
    {
        if (!_isInterstitial && !_isReward && !_isTimer)
        {
            if (!focus)
            {
                MuteAudio(!focus);
                Time.timeScale = 0;
            }
            else
            {
                MuteAudio(Data.IsMute);
                Time.timeScale = 1;
            }
        }
    }

    public void ChangeStateTimerVisible(bool state) => _isTimer = state;

    public void MuteAudio(bool state)
    {
        float value = state ? 0f : 1f;
        AudioListener.pause = state;
        AudioListener.volume = value;
    }

    public void Save() => YandexGame.SaveProgress();
}
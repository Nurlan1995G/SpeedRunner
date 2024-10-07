using System;
using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class ADTimer : MonoBehaviour
{
    [SerializeField] private float _totalSecondToShowAd = 3f; 
    [SerializeField] private TMP_Text _countdownText; 
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Canvas _canvas;

    private void Awake()
    {
        _canvas.sortingOrder = -1;
    }

    public void ShowAdWithCountdown(Action onAdShown) => 
        StartCoroutine(CountdownAndShowAd(onAdShown));

    private IEnumerator CountdownAndShowAd(Action onAdShown)
    {
        _canvasGroup.alpha = 1f;
        float countdown = _totalSecondToShowAd;
        Time.timeScale = 0;
        _canvas.sortingOrder = 1000;

        while (countdown > 0)
        {
            _countdownText.text = Mathf.CeilToInt(countdown).ToString();
            yield return new WaitForSecondsRealtime(1f); 
            countdown--;
        }

        _canvasGroup.alpha = 0f;
        _canvas.sortingOrder = -1;
        Time.timeScale = 1;
        onAdShown?.Invoke();
    }
}
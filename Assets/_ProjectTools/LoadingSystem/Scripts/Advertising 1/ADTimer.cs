using System;
using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class ADTimer : MonoBehaviour
{
    [SerializeField] private float _totalSecondToShowAd = 3f; 
    [SerializeField] private TMP_Text _countdownText; 

    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void ShowAdWithCountdown(Action onAdShown)
    {
        StartCoroutine(CountdownAndShowAd(onAdShown));
    }

    private IEnumerator CountdownAndShowAd(Action onAdShown)
    {
        _canvasGroup.alpha = 1f;
        float countdown = _totalSecondToShowAd;
        Time.timeScale = 0;

        while (countdown > 0)
        {
            _countdownText.text = Mathf.CeilToInt(countdown).ToString();
            yield return new WaitForSecondsRealtime(1f); 
            countdown--;
        }

        _canvasGroup.alpha = 0f;
        Time.timeScale = 1;
        onAdShown?.Invoke();
    }
}
using Assets._Project.Config;
using System.Collections;
using TMPro;
using UnityEngine;

public class CountdownController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countdownText;
    [SerializeField] private GameObject _barrier;
    private TimerLevel _timerLevel;
    private LogicConfig _logicConfig;
    private GameActivator _gameActivator;
    private Language _language;

    private int _countdownTime;

    public void Construct(TimerLevel timerLevel, LogicConfig logicConfig, Language language, GameActivator gameActivator)
    {
        _timerLevel = timerLevel;
        _logicConfig = logicConfig;
        _countdownTime = _logicConfig.CountdownControllerTime;
        _language = language;
        _gameActivator = gameActivator;
    }

    public void ActivateStart()
    {
        _barrier.SetActive(true);
        _countdownTime = _logicConfig.CountdownControllerTime;
        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        while (_countdownTime > 0)
        {
            _countdownText.text = _countdownTime.ToString();
            yield return new WaitForSeconds(1);
            _countdownTime--;
        }

        _countdownText.text = _language == Language.Russian ? "Вперёд!" : "GO!"; 
        _barrier.SetActive(false);

        yield return new WaitForSeconds(1);
        _countdownText.text = "";

        _timerLevel.SetStarted();
    }

    public void ResetBarrier() =>
        _barrier.SetActive(true);
}

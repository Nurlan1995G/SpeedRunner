using Assets._Project.Config;
using System;
using TMPro;
using UnityEngine;

public class TimerLevel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textTimer;
    private LogicConfig _logicConfig;

    private float _timer;
    private bool _raceStarted = false;

    public event Action ChangeLevel;

    public void Construct(LogicConfig logicConfig)
    {
        _logicConfig = logicConfig;
        _timer = logicConfig.Timer;
    }

    private void Update()
    {
        if (_raceStarted)
        {
            _timer -= Time.deltaTime;

            if (_timer <= 0)
            {
                _timer = 0;
                ChangeLevel?.Invoke();
                _raceStarted = false;
            }

            UpdateTimerDisplay();
        }
    }

    public void SetStarted()
    {
        _timer = _logicConfig.Timer;
        _raceStarted = true;
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(_timer / 60F);
        int seconds = Mathf.FloorToInt(_timer % 60F);
        int milliseconds = Mathf.FloorToInt((_timer * 100F) % 100F);

        _textTimer.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }
}

using Assets._Project.Config;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerLevel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textTimer;

    private LogicConfig _logicConfig;

    public void Construct(LogicConfig logicConfig) =>
        _logicConfig = logicConfig;

    private void Update()
    {
        _logicConfig.Timer -= Time.deltaTime;

        if (_logicConfig.Timer <= 0)
        {
            _logicConfig.Timer = 0;
            SceneManager.LoadScene("Game2"); 
        }

        UpdateTimerDisplay();
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(_logicConfig.Timer / 60F);
        int seconds = Mathf.FloorToInt(_logicConfig.Timer % 60F);
        int milliseconds = Mathf.FloorToInt((_logicConfig.Timer * 100F) % 100F);

        _textTimer.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }
}

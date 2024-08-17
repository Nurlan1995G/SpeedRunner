using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerLevel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textTimer;

    private float _timer = 30f;

    private void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0)
        {
            _timer = 0;
            SceneManager.LoadScene("Game2"); 
        }

        UpdateTimerDisplay();
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(_timer / 60F);
        int seconds = Mathf.FloorToInt(_timer % 60F);
        int milliseconds = Mathf.FloorToInt((_timer * 100F) % 100F);

        _textTimer.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }
}

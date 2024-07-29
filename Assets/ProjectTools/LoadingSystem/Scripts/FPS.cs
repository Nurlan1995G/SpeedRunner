using TMPro;
using UnityEngine;

public class FPS : MonoBehaviour
{
    [SerializeField] private TMP_Text _currentFpsLabel;
    [SerializeField] private int _frameRange = 60;

    private int[] _fpsBuffer;
    private int _fpsBufferIndex;
    private int _currentFps;

    private void Awake() => DontDestroyOnLoad(gameObject);

    private void Update()
    {
        if(_fpsBuffer == null || _frameRange != _fpsBuffer.Length)
            InitializeBuffer();

        UpdateBuffer();
        CalculateFps();

        _currentFpsLabel.text = _currentFps.ToString();

        if (_currentFps < 30)
            _currentFpsLabel.color = Color.red;

        if (_currentFps >= 30 && _currentFps < 59)
            _currentFpsLabel.color = Color.yellow;

        if (_currentFps >= 60)
            _currentFpsLabel.color = Color.green;
    }

    private void InitializeBuffer()
    {
        if (_frameRange <= 0)
            _frameRange = 1;

        _fpsBuffer = new int[_frameRange];
        _fpsBufferIndex = 0;
    }

    private void UpdateBuffer()
    {
        _fpsBuffer[_fpsBufferIndex++] = (int)(1 / Time.unscaledDeltaTime);

        if (_fpsBufferIndex >= _frameRange)
            _fpsBufferIndex = 0;
    }

    private void CalculateFps()
    {
        int sum = 0;

        for (int i = 0; i < _frameRange; i++)
        {
            sum += _fpsBuffer[i];
        }

        _currentFps = sum / _frameRange;
    }
}
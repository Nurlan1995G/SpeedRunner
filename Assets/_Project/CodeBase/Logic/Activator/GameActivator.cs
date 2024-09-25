using UnityEngine;
using UnityEngine.UI;

public class GameActivator : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _shopButton;

    private bool _isGamePaused = true;

    public bool IsGamePaused => _isGamePaused;

    private void OnEnable()
    {
        _startButton.onClick.AddListener(ActivateGame);
        _shopButton.onClick.AddListener(DeactivateGame);
    }

    private void OnDisable()
    {
        _shopButton.onClick.RemoveListener(DeactivateGame);
        _startButton.onClick.RemoveListener(ActivateGame);
    }

    private void ActivateGame() => 
        _isGamePaused = false;

    private void DeactivateGame() => 
        _isGamePaused = true;
}
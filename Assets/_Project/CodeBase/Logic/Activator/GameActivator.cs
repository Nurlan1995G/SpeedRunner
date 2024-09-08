using UnityEngine;
using UnityEngine.UI;

public class GameActivator : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _shopButton;

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
        Time.timeScale = 1;

    private void DeactivateGame() =>
        Time.timeScale = 0;
}
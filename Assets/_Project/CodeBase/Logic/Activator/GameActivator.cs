using UnityEngine;
using UnityEngine.UI;

public class GameActivator : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _shopButton;

    private void OnEnable()
    {
        _startButton.onClick.AddListener(ActivateGame);
        _shopButton.onClick.AddListener(DeacrivateGame);
    }

    private void OnDisable()
    {
        _shopButton.onClick.RemoveListener(DeacrivateGame);
        _startButton.onClick.RemoveListener(ActivateGame);
    }

    private void ActivateGame() =>
        Time.timeScale = 1;

    private void DeacrivateGame() =>
        Time.timeScale = 0;
}
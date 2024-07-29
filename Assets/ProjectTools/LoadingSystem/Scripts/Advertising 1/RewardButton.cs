using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardButton : MonoBehaviour
{
    [SerializeField] private int _reward;
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _value;

    private void Awake() => _value.text = "+" + _reward.ToString();

    private void OnEnable() => _button.onClick.AddListener(OnClick);

    private void OnDisable() => _button.onClick.RemoveListener(OnClick);

    private void OnClick() => YandexSDK.Instance.ShowVideoAd(SetReward);

    private  void SetReward() => Wallet.Add(_reward);
}
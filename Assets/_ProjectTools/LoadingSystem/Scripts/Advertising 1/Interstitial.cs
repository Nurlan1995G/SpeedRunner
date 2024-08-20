using UnityEngine;

public class Interstitial : MonoBehaviour
{
    private YandexSDK _yandexSDK;

    private void Awake()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        _yandexSDK = YandexSDK.Instance;
        _yandexSDK.ShowInterstitial();
#endif
    }
}
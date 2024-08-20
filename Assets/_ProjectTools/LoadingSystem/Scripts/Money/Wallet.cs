using System;
using UnityEngine;

public static class Wallet
{
    private const int MinValue = 0;

    public static int CoinValue { get; private set; }

    public static event Action<int> CoinChanged;

    static Wallet()
    {
        if (YandexSDK.Instance != null)
            CoinValue = YandexSDK.Instance.Data.Coin;
    }

    public static void Add(int value)
    {
        CoinValue += value;
        YandexSDK.Instance.Data.Coin = CoinValue;
        CoinChanged?.Invoke(CoinValue);
        YandexSDK.Instance.Save();
    }

    public static void Take(int value)
    {
        CoinValue = Mathf.Max(CoinValue - value, MinValue);
        YandexSDK.Instance.Data.Coin = CoinValue;
        CoinChanged?.Invoke(CoinValue);
        YandexSDK.Instance.Save();
    }
}
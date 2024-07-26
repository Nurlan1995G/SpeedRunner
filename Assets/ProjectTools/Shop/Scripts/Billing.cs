using System;
using YG;
using YG.Utils.Pay;

public static class Billing
{
    private static Action<Purchase> _onSuccessfulCallback;
    private static Purchase _purchase;

    //Купить товар
    public static void PurchaseProduct(string id, Action<Purchase> onSuccessfulCallback = null, Action<string> onErrorCallback = null)
    {
        _purchase = YandexGame.PurchaseByID(id);

        if (_purchase != null)
        {
            _onSuccessfulCallback = onSuccessfulCallback;
            YandexGame.PurchaseSuccessEvent += SuccessPurchased;
            YandexGame.BuyPayments(id);
        }
        else
            onErrorCallback?.Invoke(id);
    }

    private static void SuccessPurchased(string id)
    {
        _onSuccessfulCallback?.Invoke(_purchase);
        YandexGame.PurchaseSuccessEvent -= SuccessPurchased;
    }

    //Применить товар
    public static void ConsumeProduct(string id, Action onSuccessfulCallback = null)
    {
        YandexGame.ConsumePurchaseByID(id);
        onSuccessfulCallback?.Invoke();
    }

    public static void GetPurchasedProducts(Action<Purchase[]> onSuccessfulCallback, Action<string> onErrorCallback)
    {
        if (YandexGame.purchases.Length > 0)
            onSuccessfulCallback?.Invoke(YandexGame.purchases);
        else
            onErrorCallback?.Invoke(YandexGame.purchases.Length.ToString());
    }
    
    public static void ConsumePurchases() => YandexGame.ConsumePurchases();
}
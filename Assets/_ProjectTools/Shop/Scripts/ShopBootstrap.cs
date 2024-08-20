using System.Collections;
using UnityEngine;

public class ShopBootstrap : MonoBehaviour
{
    [SerializeField] private Shop _shop;
    [SerializeField] private MainMenu _mainMenu;

    private void Awake() => StartCoroutine(InitializeShop());

    private IEnumerator InitializeShop()
    {
        OpenItemChecker openItemChecker = new OpenItemChecker();
        SelectedItemChecker selectedItemChecker = new SelectedItemChecker();
        ItemSelector itemSelector = new ItemSelector();
        ItemUnlocker itemUnlocker = new ItemUnlocker();

        _shop.Initialize(openItemChecker, selectedItemChecker, itemSelector, itemUnlocker);
        Billing.ConsumePurchases();
        yield return new WaitUntil(() => _shop.IsInitialized);
        _mainMenu.Initialize();
        _mainMenu.Draw();
    }
}
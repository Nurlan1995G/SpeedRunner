using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopCategoryButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _mask;

    public event Action Click;

    private void OnEnable() => _button.onClick.AddListener(OnClick);

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    public void ChangeState(bool state) => _button.interactable = !state;

    public void Select()
    {
        ChangeState(true);
        _mask.gameObject.SetActive(true);
    }

    public void UnSelect()
    {
        ChangeState(false);
        _mask.gameObject.SetActive(false);
    }

    private void OnClick() => Click?.Invoke();
}
using UnityEngine;
using UnityEngine.EventSystems;

public class JumpButtonHeandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool IsPointerDown { get; private set; }
    public bool IsPointerUp { get; private set; }

    public void OnPointerDown(PointerEventData eventData)
    {
        IsPointerDown = true;
        IsPointerUp = false;
    }

    public void PointerDownDisabling() => IsPointerDown = false;

    public void OnPointerUp(PointerEventData eventData)
    {
        IsPointerDown = false;
        IsPointerUp = true;
    }
}

using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Interactable : MonoBehaviour
{
    protected abstract void Interact(Collider other);
    protected abstract void InteractExit(Collider other);

    private void OnTriggerEnter(Collider other) =>
        Interact(other);

    private void OnTriggerExit(Collider other) =>
        InteractExit(other);
}

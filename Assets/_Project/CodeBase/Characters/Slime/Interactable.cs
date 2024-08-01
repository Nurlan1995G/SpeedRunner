using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Interactable : MonoBehaviour
{
    protected abstract void Interact(Collider other);

    private void OnTriggerEnter(Collider other) =>
        Interact(other);
}

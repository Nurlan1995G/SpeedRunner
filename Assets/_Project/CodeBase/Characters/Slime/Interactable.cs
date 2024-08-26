using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class BaseInteractable : MonoBehaviour { }

public abstract class Interactable : BaseInteractable, IInteractableEnter, IInteractableExit
{
    public abstract void InteractEnter(Collider collider);
    public abstract void InteractExit(Collider collider);

    private void OnTriggerEnter(Collider other) =>
        InteractEnter(other);

    private void OnTriggerExit(Collider other) =>
        InteractExit(other);
}

public abstract class InteractableEnter : BaseInteractable, IInteractableEnter
{
    public abstract void InteractEnter(Collider other);

    private void OnTriggerEnter(Collider other) =>
        InteractEnter(other);
}

public abstract class InteractableExit : BaseInteractable, IInteractableExit
{
    public abstract void InteractExit(Collider collider);

    private void OnTriggerExit(Collider other) =>
        InteractExit(other);
}

public interface IInteractableExit
{
    void InteractExit(Collider collider);
}

public interface IInteractableEnter
{
    void InteractEnter(Collider collider);
}

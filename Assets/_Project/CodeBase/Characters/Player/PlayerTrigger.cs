using UnityEngine;

public class PlayerTrigger : Interactable
{
    [SerializeField] private Player _player;

    protected override void Interact(Collider other)
    {
        if (other.TryGetComponent(out BurningBox burningBox))
            _player.Respawn(true);
    }
}
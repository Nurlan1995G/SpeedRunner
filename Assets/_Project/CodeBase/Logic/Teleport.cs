using UnityEngine;

public class Teleport : InteractableEnter
{
    public override void InteractEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
            player.Respawn();
    }
}

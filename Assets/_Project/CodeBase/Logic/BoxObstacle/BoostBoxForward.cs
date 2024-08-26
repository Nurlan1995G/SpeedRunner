using UnityEngine;

public class BoostBoxForward : InteractableEnter
{
    public override void InteractEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerMover playerMover))
            playerMover.ActivateSpeedBoost();
    }
}

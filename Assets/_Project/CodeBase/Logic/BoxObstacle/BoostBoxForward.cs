using UnityEngine;

public class BoostBoxForward : InteractableEnter
{
    public override void InteractEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerMover playerMover))
            playerMover.ActivateSpeedBoost();

        if (other.TryGetComponent(out BotController botController))
            botController.ActivateBoostForward();
    }
}

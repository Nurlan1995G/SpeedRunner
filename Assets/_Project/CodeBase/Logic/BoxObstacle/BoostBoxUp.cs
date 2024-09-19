using System;
using UnityEngine;

public class BoostBoxUp : Interactable
{
    public event Action PlayerBoostJump;

    public override void InteractEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMover playerMover))
            playerMover.BoostBoxUp();

        if (other.TryGetComponent(out BotController botController))
            botController.BoostBoxUp();
    }

    public override void InteractExit(Collider other)
    {
        if(other.TryGetComponent(out PlayerMover playerMover))
            PlayerBoostJump?.Invoke();
    }
}

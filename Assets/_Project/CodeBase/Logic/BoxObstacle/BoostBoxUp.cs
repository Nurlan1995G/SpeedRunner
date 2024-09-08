using System;
using UnityEngine;

public class BoostBoxUp : Interactable
{
    public event Action PlayerBoostJump;

    public override void InteractEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMover playerMover))
            playerMover.InitBoostBoxUp(this);
    }

    public override void InteractExit(Collider other)
    {
        if(other.TryGetComponent(out PlayerMover playerMover))
            PlayerBoostJump?.Invoke();
    }
}

using System;
using UnityEngine;

public class BoostBoxUp : Interactable
{
    public event Action BoostJump;

    public override void InteractEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMover playerMover))
            playerMover.SetBoostBoxUp(this);

        if (other.TryGetComponent(out BotView bot))
            bot.SetBoostBoxUp(this);
    }

    public override void InteractExit(Collider other)
    {
        if(other.TryGetComponent(out PlayerMover playerMover))
            BoostJump?.Invoke();

        if (other.TryGetComponent(out BotView bot))
            BoostJump?.Invoke();
    }
}

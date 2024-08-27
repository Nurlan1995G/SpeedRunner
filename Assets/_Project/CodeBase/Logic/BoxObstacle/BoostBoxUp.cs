using System;
using UnityEngine;

public class BoostBoxUp : Interactable
{
    public event Action PlayerBoostJump;
    public event Action BotBoostJump;

    public override void InteractEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMover playerMover))
            playerMover.InitBoostBoxUp(this);

        /*if (other.TryGetComponent(out BotView bot))
            bot.InitBoostBoxUp(this);*/
    }

    public override void InteractExit(Collider other)
    {
        if(other.TryGetComponent(out PlayerMover playerMover))
            PlayerBoostJump?.Invoke();

        /*if (other.TryGetComponent(out BotView bot))
            BotBoostJump?.Invoke();*/
    }
}

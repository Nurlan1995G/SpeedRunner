using UnityEngine;

public class JumpTrigger : InteractableEnter
{
    public override void InteractEnter(Collider other)
    {
        if (other.TryGetComponent(out BotController botController))
            botController.JumpTrigger();
    }
}

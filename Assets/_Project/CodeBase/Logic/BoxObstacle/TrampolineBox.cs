using UnityEngine;

public class TrampolineBox : InteractableEnter
{
    public override void InteractEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerJumper playerJumper))
            playerJumper.TrampolineJumpUp();

        if (other.TryGetComponent(out BotController botController))
            botController.TrampolineJumpUp();
    }
}

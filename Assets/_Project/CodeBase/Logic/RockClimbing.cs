using UnityEngine;

public class RockClimbing : Interactable
{
    public override void InteractEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMover playerMover))
        {
            Vector3 normal = (other.transform.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(-normal);
            playerMover.StartClimbing(targetRotation);
        }

        if (other.TryGetComponent(out BotController botController))
            botController.StartClimbing();
    }

    public override void InteractExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerMover playerMover))
            playerMover.StopClimbing();

        if (other.TryGetComponent(out BotController botController))
            botController.StopClimbing();
    }
}

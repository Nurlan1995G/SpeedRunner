using UnityEngine;

public class Finish : InteractableEnter
{
    private RaceManager _raceManager;

    public void Construct(RaceManager raceManager) =>
        _raceManager = raceManager;

    public override void InteractEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.SetFinish(true);
            _raceManager.RegisterFinish(player);
        }

        if (other.TryGetComponent(out BotController botController))
        {
            botController.StopMovement();
            _raceManager.RegisterFinish(botController);
        }
    }
}

using Assets._Project.CodeBase.Characters.Interface;
using UnityEngine;

public class Finish : InteractableEnter
{
    private RaceManager _raceManager;

    public void Construct(RaceManager raceManager) =>
        _raceManager = raceManager;

    public override void InteractEnter(Collider other)
    {
        if (other.TryGetComponent(out IRespawned character))
        {
            if (character is BotController bot)
                bot.StopMovement();

            _raceManager.RegisterFinish(character);
        }
    }
}

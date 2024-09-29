using UnityEngine;

public class Teleport : InteractableEnter
{
    [SerializeField] private LevelLoader _levelLoader;

    public override void InteractEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.ActivateForRace();
            _levelLoader.DeactivateFlags();
            _levelLoader.DeactivateCoins();
        }
    }
}

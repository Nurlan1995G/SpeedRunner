using UnityEngine;

public class PlayerTrigger : Interactable
{
    [SerializeField] private Player _player;

    protected override void Interact(Collider other)
    {
        if (other.TryGetComponent(out BurningBox burningBox))
            _player.Respawn(true);

        if (other.TryGetComponent(out Coin coin))
        {
            _player.SetScore(10);
            coin.SetEffectCoin();
        }

        if(other.TryGetComponent(out BoostBox boostBox))
        {

        }
    }
}
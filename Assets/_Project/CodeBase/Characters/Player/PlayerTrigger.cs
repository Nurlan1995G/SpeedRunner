using UnityEngine;

public class PlayerTrigger : Interactable
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerMover _playerMover;

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
            _playerMover.ActivateSpeedBoost(_player.GameConfig.CharacterData.BoostMultiplier,
                _player.GameConfig.CharacterData.BoostDuration);
        }

        if (other.TryGetComponent(out FlagPoint flagPoint))
            flagPoint.FlagChange(_player);
    }
}
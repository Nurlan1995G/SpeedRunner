using UnityEngine;

public class PlayerTrigger : Interactable
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerMover _playerMover;

    protected override void Interact(Collider other)
    {
        if (other.TryGetComponent(out Coin coin))
        {
            _player.SetScore(10);
            coin.SetEffectCoin();
        }

        if(other.TryGetComponent(out BoostBoxForward boostBox))
        {
            _playerMover.ActivateSpeedBoost(_player.CharacterData.BoostMultiplier,
                _player.CharacterData.BoostDuration);
        }

        if (other.TryGetComponent(out FlagPoint flagPoint))
            flagPoint.FlagChange(_player);
    }

    protected override void InteractExit(Collider other)
    {
        if (other.TryGetComponent(out BoostBoxUp boostBoxUp))
        {
            boostBoxUp.SetPowerJump(_player, _playerMover);
        }
    }
}
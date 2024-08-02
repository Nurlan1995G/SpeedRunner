using Assets.Project.CodeBase.Player.UI;
using UnityEngine;

public class PlayerTrigger : Interactable
{
    [SerializeField] private Player _playerView;
    [SerializeField] private EffectCoin _canvasCoinEffect;

    protected override void Interact(Collider other)
    {
        
    }

    private void ShowCoinEffect() =>
        _canvasCoinEffect.ActivateCoin();
}
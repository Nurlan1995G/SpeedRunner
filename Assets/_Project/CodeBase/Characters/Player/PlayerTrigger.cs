using Assets._Project.CodeBase.Slime.Interface;
using Assets.Project.CodeBase.Player.UI;
using UnityEngine;

public class PlayerTrigger : Interactable
{
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private EffectCoin _canvasCoinEffect;

    protected override void Interact(Collider other)
    {
        if (other.TryGetComponent(out IDestroyableSlime slimeModel))
        {
            if (_playerView.ScoreLevel > slimeModel.ScoreLevel && slimeModel.ScoreLevel > 1)
            {
                _playerView.AddScore(slimeModel.ScoreLevel);
                slimeModel.Destroy();
                ShowCoinEffect();
            }
        }
    }

    private void ShowCoinEffect() =>
        _canvasCoinEffect.ActivateCoin();
}
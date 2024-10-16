using UnityEngine;

public class Coin : InteractableEnter
{
    [SerializeField] private GameObject _canvasCoin;
    [SerializeField] private ParticleSystem _coinEffect;

    public override void InteractEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            player.SetScore(25);
            SetEffectCoin();
        }
    }

    public void ActivateCoin(bool isActivate) =>
        _canvasCoin.SetActive(isActivate);

    private void SetEffectCoin()
    {
        if (_canvasCoin.activeSelf)
        {
            ActivateCoin(false);
            _coinEffect.Play();
        }
    } 
}

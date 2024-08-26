using UnityEngine;

public class Coin : InteractableEnter
{
    [SerializeField] private GameObject _canvasCoin;
    [SerializeField] private ParticleSystem _coinEffect;

    public override void InteractEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            player.SetScore(10);
            SetEffectCoin();
        }
    }

    private void SetEffectCoin()
    {
        if (_canvasCoin.activeSelf)
        {
            _canvasCoin.SetActive(false);
            _coinEffect.Play();
        }
    } 
}

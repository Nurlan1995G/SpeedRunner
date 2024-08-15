using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private GameObject _canvasCoin;
    [SerializeField] private ParticleSystem _coinEffect;

    public void SetEffectCoin()
    {
        if (_canvasCoin.activeSelf)
        {
            _canvasCoin.SetActive(false);
            _coinEffect.Play();
        }
    } 
}

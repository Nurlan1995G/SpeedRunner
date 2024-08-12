using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private GameObject _canvasCoin;
    [SerializeField] private ParticleSystem _coinEffect;

    private void Start()
    {
        _coinEffect.gameObject.SetActive(false);
    }

    public void SetEffectCoin()
    {
        _canvasCoin.SetActive(false);
        _coinEffect.gameObject.SetActive(true);
        _coinEffect.Play();
    } 
}

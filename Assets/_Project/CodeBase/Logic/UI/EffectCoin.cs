using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Project.CodeBase.Player.UI
{
    public class EffectCoin : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 1f;
        [SerializeField] private float _fadeSpeed = 5f;
        [SerializeField] private List<Coin> _coins;

        private Vector3 _initialPosition;
        private bool _isFadingOut = false;

        private void Start() =>
            _initialPosition = new Vector3(120,80,0);

        void Update()
        {
            foreach (var coin in _coins)
            {
                if (coin.gameObject.activeSelf)
                {
                    coin.transform.localPosition += Vector3.up * _moveSpeed * Time.deltaTime;

                    if (_isFadingOut)
                    {
                        var color = coin.ImageCoin.color;
                        color.a -= _fadeSpeed * Time.deltaTime;
                        coin.ImageCoin.color = color;

                        if (coin.ImageCoin.color.a <= 0f)
                            ResetCoin(coin);
                    }
                }
            }
        }

        private void ResetCoin(Coin coin)
        {
            var color = coin.ImageCoin.color;
            color.a = 1f;
            coin.ImageCoin.color = color;
            coin.transform.localPosition = _initialPosition;
            coin.gameObject.SetActive(false);
        }

        public void ActivateCoin()
        {
            Coin availableCoin = _coins.FirstOrDefault(c => !c.gameObject.activeSelf);

            if (availableCoin != null)
            {
                var color = availableCoin.ImageCoin.color;
                color.a = 1f;
                availableCoin.ImageCoin.color = color;
                availableCoin.transform.localPosition = _initialPosition;
                availableCoin.gameObject.SetActive(true);
                _isFadingOut = true;
            }
        }
    }
}

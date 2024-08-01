using Assets.Project.CodeBase.Player.Respawn;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Project.CodeBase.Player.UI
{
    public class UIPopup : MonoBehaviour
    {
        [SerializeField] private Button respawnButton;
        [SerializeField] private ADTimer _adTimer;

        private RespawnSlime _respawnPlayer;

        public void Initialize(RespawnSlime respawnPlayer) =>
            _respawnPlayer = respawnPlayer;

        private void OnEnable() =>
            respawnButton.onClick.AddListener(OnRespawn);

        private void OnDisable() => 
            respawnButton.onClick.RemoveListener(OnRespawn);

        private void OnRespawn()
        {
            _respawnPlayer.Respawn();
            gameObject.SetActive(false);
        }
    }
}

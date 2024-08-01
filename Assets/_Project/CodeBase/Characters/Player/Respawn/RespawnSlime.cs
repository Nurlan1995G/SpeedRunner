using Assets.Project.CodeBase.Player.UI;

namespace Assets.Project.CodeBase.Player.Respawn
{
    public class RespawnSlime
    {
        private UIPopup _uiPopup;
        private PlayerView _playerView;

        public RespawnSlime(UIPopup uiPopup, PlayerView playerView)
        {
            _uiPopup = uiPopup;
            _playerView = playerView;
        }

        public void SelectAction()
        {
            _uiPopup.Initialize(this);
            _uiPopup.gameObject.SetActive(true);
        }

        public void Respawn() =>
            _playerView.Teleport();
    }
}

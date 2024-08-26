using UnityEngine;

public class FlagPoint : InteractableEnter
{
    [SerializeField] private GameObject _redFlag, _blueFlag;
    [SerializeField] private ParticleSystem _effectPointFlag;

    public override void InteractEnter(Collider other)
    {
        if (other.TryGetComponent(out BotView bot))
            bot.SetRespawnPosition(transform.position);

        if (other.TryGetComponent(out Player player))
            FlagChange(player);
    }

    private void FlagChange(Player player)
    {
        if (_blueFlag.activeSelf == false)
        {
            _effectPointFlag.Play();
            _redFlag.SetActive(false);
            _blueFlag.SetActive(true);
            player.RespawnPosition(transform.position);
        }
    }
}

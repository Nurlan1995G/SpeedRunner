using UnityEngine;

public class FlagPoint : Interactable
{
    [SerializeField] private GameObject _redFlag, _blueFlag;
    [SerializeField] private ParticleSystem _effectPointFlag;

    public void FlagChange(Player player)
    {
        if (_blueFlag.activeSelf == false)
        {
            _effectPointFlag.Play();
            _redFlag.SetActive(false);
            _blueFlag.SetActive(true);
            player.RespawnPosition(transform.position);
        }
    }

    protected override void Interact(Collider other)
    {
        if (other.TryGetComponent(out BotView bot))
            bot.SetRespawnPosition(transform.position);
    }

    protected override void InteractExit(Collider other)
    {
    }
}

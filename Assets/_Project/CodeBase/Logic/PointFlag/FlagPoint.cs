using UnityEngine;

public class FlagPoint : MonoBehaviour
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

    private void OnTriggerEnter(Collider other)
    {
        
    }
}

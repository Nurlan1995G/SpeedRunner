using UnityEngine;

public class FlagPoint : MonoBehaviour
{
    [SerializeField] private GameObject _redFlag, _blueFlag;
    [SerializeField] private ParticleSystem _effectPointFlag;

    private PositionStaticData _staticData;

    public void Construct(PositionStaticData staticData)
    {
        _staticData = staticData;
    }

    public void FlagChange(Player player)
    {
        if (_blueFlag.activeSelf == false)
        {
            _effectPointFlag.Play();
            _redFlag.SetActive(false);
            _blueFlag.SetActive(true);
            _staticData.InitPlayerPosition = transform.position;
        }
    }
}

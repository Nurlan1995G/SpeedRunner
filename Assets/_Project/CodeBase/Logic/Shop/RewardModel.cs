using UnityEngine;

public class RewardModel : MonoBehaviour
{
    [SerializeField] private ItemInfo _itemInfo;

    public ItemInfo ItemInfo => _itemInfo;

    public void ChangeState(bool state) => gameObject.SetActive(state);
}

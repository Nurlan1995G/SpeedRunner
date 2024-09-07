using UnityEngine;

public class RewardModel : MonoBehaviour
{
    [SerializeField] private ItemInfo _itemInfo;
    [SerializeField] private Animator _animator;

    public ItemInfo ItemInfo => _itemInfo;
    public Animator Animator => _animator;

    public void ChangeState(bool state) => gameObject.SetActive(state);
}

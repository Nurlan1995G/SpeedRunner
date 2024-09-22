using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.CodeBase.Player.Skin
{
    public class PlayerSkin : MonoBehaviour
    {
        [field: SerializeField] public List<RewardModel> Trails;

        [field: SerializeField] public ItemInfo ItemInfo { get; private set; }
        [field: SerializeField] public Animator Animator { get; private set; }

        public void ChangeState(bool state) => gameObject.SetActive(state);
    }
}

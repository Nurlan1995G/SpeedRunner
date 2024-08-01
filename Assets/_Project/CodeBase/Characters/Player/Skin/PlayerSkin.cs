using UnityEngine;

namespace Assets._Project.CodeBase.Player.Skin
{
    public class PlayerSkin : MonoBehaviour
    {
        [field: SerializeField] public ItemInfo ItemInfo;

        public void ChangeState(bool state) => gameObject.SetActive(state);
    }
}

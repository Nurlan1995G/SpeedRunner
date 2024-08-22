using TMPro;
using UnityEngine;

public class BotNickName : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;

    public void Set(string name) =>
        _name.text = name;
}
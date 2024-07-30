using Assets._Project.Config;
using UnityEngine;

public class PlayerJumper : MonoBehaviour
{
    private PlayerData _playerData;

    public void Construct(PlayerData playerData)
    {
        _playerData = playerData;
    }
}

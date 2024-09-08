using UnityEngine;

[CreateAssetMenu(fileName = "PositionLevel")]
public class PositionStaticData : ScriptableObject
{
    [Header("Player position")]
    public Vector3 InitPlayerPosition;
}

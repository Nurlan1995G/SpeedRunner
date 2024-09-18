using UnityEngine;

[CreateAssetMenu(fileName = "PositionLevel")]
public class PositionStaticData : ScriptableObject
{
    [Header("Player position")]
    public Vector3 InitPlayerPosition;
    [Header("PositionWinner")]
    public Vector3 OnePosition;
    public Vector3 TwoPosition;
    public Vector3 ThreePosition;
}

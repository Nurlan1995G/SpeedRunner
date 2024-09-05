using UnityEngine;

[CreateAssetMenu(fileName = "PositionLevel")]
public class PositionStaticData : ScriptableObject
{
    [Header("Sharks position")]
    public Vector3 InitSharkOnePosition;
    public Vector3 InitSharkTwoPosition;
    public Vector3 InitSharkThreePosition;
    public Vector3 InitSharkFourPosition;
    public Vector3 InitSharkFivePosition;
    public Vector3 InitSharkSixPosition;
    public Vector3 InitSharkSevenPosition;
    public Vector3 InitSharkEightPosition;
    public Vector3 InitSharkNinePosition;
    [Header("Player position")]
    public Vector3 InitPlayerPosition;
}

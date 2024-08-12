using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PositionStaticData))]
public class LevelStaticDataEditor : UnityEditor.Editor
{
    private const string SharkEnemy1 = "SharkPoint1";
    private const string SharkEnemy2 = "SharkPoint2";
    private const string SharkEnemy3 = "SharkPoint3";
    private const string SharkEnemy4 = "SharkPoint4";
    private const string SharkEnemy5 = "SharkPoint5";
    private const string SharkEnemy6 = "SharkPoint6";
    private const string SharkEnemy7 = "SharkPoint7";
    private const string SharkEnemy8 = "SharkPoint8";
    private const string SharkEnemy9 = "SharkPoint9";
    private const string PlayerPointTag = "SpawnPointPlayer";

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PositionStaticData levelData = (PositionStaticData)target;

        if (GUILayout.Button("Collect"))
        {
            //levelData.InitSharkOnePosition = GameObject.FindWithTag(SharkEnemy1).transform.position;
            //levelData.InitSharkTwoPosition = GameObject.FindWithTag(SharkEnemy2).transform.position;
            //levelData.InitSharkThreePosition = GameObject.FindWithTag(SharkEnemy3).transform.position;
            //levelData.InitSharkFourPosition = GameObject.FindWithTag(SharkEnemy4).transform.position;
            //levelData.InitSharkFivePosition = GameObject.FindWithTag(SharkEnemy5).transform.position;
            //levelData.InitSharkSixPosition = GameObject.FindWithTag(SharkEnemy6).transform.position;
            //levelData.InitSharkSevenPosition = GameObject.FindWithTag(SharkEnemy7).transform.position;
            //levelData.InitSharkEightPosition = GameObject.FindWithTag(SharkEnemy8).transform.position;
            //levelData.InitSharkNinePosition = GameObject.FindWithTag(SharkEnemy9).transform.position;
            levelData.InitPlayerPosition = GameObject.FindWithTag(PlayerPointTag).transform.position;
        }

        EditorUtility.SetDirty(target);
    }
}

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PositionStaticData))]
public class LevelStaticDataEditor : UnityEditor.Editor
{
    private const string PlayerPointTag = "SpawnPointPlayer";
    private const string OnePointTag = "OnePointTag";
    private const string TwoPointTag = "TwoPointTag";
    private const string ThreePointTag = "ThreePointTag";

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PositionStaticData levelData = (PositionStaticData)target;

        if (GUILayout.Button("Collect"))
        {
            levelData.InitPlayerPosition = GameObject.FindWithTag(PlayerPointTag).transform.position;
            levelData.OnePosition = GameObject.FindWithTag(OnePointTag).transform.position;
            levelData.TwoPosition = GameObject.FindWithTag(TwoPointTag).transform.position;
            levelData.ThreePosition = GameObject.FindWithTag(ThreePointTag).transform.position;
        }

        EditorUtility.SetDirty(target);
    }
}

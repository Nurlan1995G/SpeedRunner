using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PositionStaticData))]
public class LevelStaticDataEditor : UnityEditor.Editor
{
    private const string PlayerPointTag = "SpawnPointPlayer";

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PositionStaticData levelData = (PositionStaticData)target;

        if (GUILayout.Button("Collect"))
        {
            levelData.InitPlayerPosition = GameObject.FindWithTag(PlayerPointTag).transform.position;
        }

        EditorUtility.SetDirty(target);
    }
}

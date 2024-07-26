#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using YG;

public class PackageInstaller : MonoBehaviour
{
    [MenuItem("Installer/Initialize Project")]
    public static void InitializeProject()
    {
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.WebGL, BuildTarget.WebGL);
        PlayerSettings.SetUseDefaultGraphicsAPIs(BuildTarget.WebGL, false);
        PlayerSettings.WebGL.compressionFormat = WebGLCompressionFormat.Brotli;
        PlayerSettings.WebGL.nameFilesAsHashes = true;
        PlayerSettings.WebGL.dataCaching = false;
        PlayerSettings.WebGL.decompressionFallback = true;
        Application.runInBackground = true;
        PlayerSettings.WebGL.template = "FullscreenWindow";
        AddSceneInitialized();

        Debug.Log("WebGL project settings initialization completed.");
    }

    [MenuItem("Installer/Clear Save")]
    public static void ClearSave()
    {
        YandexGame.ResetSaveProgress();
        YandexGame.SaveProgress();
    }

    private static void AddSceneInitialized()
    {
        EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
        string scenePath = "Assets/ProjectTools/Scenes/InitializeScene.unity";
        bool sceneAlreadyAdded = false;

        foreach (EditorBuildSettingsScene buildScene in scenes)
        {
            if (buildScene.path == scenePath)
            {
                sceneAlreadyAdded = true;
                break;
            }
        }

        if (!sceneAlreadyAdded)
        {
            EditorBuildSettingsScene[] newScenes = new EditorBuildSettingsScene[scenes.Length + 1];
            scenes.CopyTo(newScenes, 0);
            newScenes[newScenes.Length - 1] = new EditorBuildSettingsScene(scenePath, true);

            EditorBuildSettings.scenes = newScenes;

            BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, "BuildTargetPath", BuildTarget.StandaloneWindows, BuildOptions.None);

            Debug.Log("Scene added to Build Settings.");
        }
        else
        {
            Debug.Log("Scene is already in Build Settings.");
        }
    }
}
#endif
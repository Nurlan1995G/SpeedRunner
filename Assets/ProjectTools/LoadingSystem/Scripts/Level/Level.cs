using System;
using UnityEngine.SceneManagement;

public static class Level
{
    public static int CurrentLevel { get; private set; }
    public static int CurrentLevelIndex { get; private set; }

    public static event Action<float> ProgressChanged;
    public static event Action IndexChanged;


    static Level()
    {
        if (YandexSDK.Instance != null)
        {
            CurrentLevel = YandexSDK.Instance.Data.CurrentLevel;
            CurrentLevelIndex = YandexSDK.Instance.Data.CurrentLevelIndex;
            LoadLevel(CurrentLevel);
        }
    }

    public static void SetNextLevel()
    {
        CurrentLevel++;
        SetNextIndexLevel();

        if (CurrentLevel > SceneManager.sceneCountInBuildSettings - 1)
            CurrentLevel = 1;

        YandexSDK.Instance.Data.CurrentLevel = CurrentLevel;
        YandexSDK.Instance.Save();

        LoadLevel(CurrentLevel);
    }

    public static void SetNextIndexLevel()
    {
        CurrentLevelIndex++;
        YandexSDK.Instance.Data.CurrentLevelIndex = CurrentLevelIndex;
        YandexSDK.Instance.Save();
        IndexChanged?.Invoke();
    }

    public static void SetProgress(float progress)
    {
        ProgressChanged?.Invoke(progress);
    }

    private static void LoadLevel(int index) => SceneManager.LoadScene(index);
}
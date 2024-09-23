using Assets._Project.CodeBase.Infrastracture;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private List<LevelScene> _levelsScene;

    private TimerLevel _timerLevel;
    private List<BotController> _botControllers;
    private List<BotView> _botViews;
    private CountdownController _countdownController;
    private Player _player;
    private NavMeshSurface _navMeshSurface;

    private int _currentLevelIndex;

    public void Construct(TimerLevel timerLevel, List<BotController> botControllers, List<BotView> botViews,
    CountdownController countdownController, Player player, NavMeshSurface navMeshSurface)
    {
        _timerLevel = timerLevel;
        _botControllers = botControllers;
        _botViews = botViews;
        _countdownController = countdownController;
        _player = player;
        _navMeshSurface = navMeshSurface;
    }

    private void OnEnable() => 
        _timerLevel.ChangeLevel += OnChangeLevel;

    private void OnDisable() =>
        _timerLevel.ChangeLevel -= OnChangeLevel;

    public void StartLevelSequence()
    {
        LoadNextLevel();
        SetupLevel();
    }

    private void OnChangeLevel()
    {
        DeactivateLevel();
        LoadNextLevel();
        SetupLevel();
    }

    private void LoadNextLevel()
    {
        _currentLevelIndex = (_currentLevelIndex + 1) % _levelsScene.Count;
        //_currentLevelIndex = Random.Range(0, _levelsScene.Count);
        ActivateScene(_currentLevelIndex);
    }

    private void SetupLevel()
    {
        LevelScene activeLevel = _levelsScene[_currentLevelIndex];
        List<PointSpawnZone> pointSpawnZones = activeLevel.PointSpawnZones;
        List<TriggerZone> triggerZones = activeLevel.TriggerZones;
        List<FlagPoint> flagPoints = activeLevel.FlagPoints;

        _player.ActivateForRace();

        InitSpawnZones(pointSpawnZones);
        ActivateBots(true);
        InitializeBots(pointSpawnZones);
        InitTriggerZones(pointSpawnZones, triggerZones);
        DeactivateFlags(flagPoints);

        _countdownController.ResetBarrier();
        _countdownController.ActivateStart();
    }

    private void ActivateScene(int levelIndex)
    {
        for (int i = 0; i < _levelsScene.Count; i++)
        {
            _levelsScene[i].gameObject.SetActive(i == levelIndex);
            _levelsScene[i].SetBusy(i == levelIndex);
        }

        _navMeshSurface.BuildNavMesh();
    }

    private void InitSpawnZones(List<PointSpawnZone> spawnZones)
    {
        foreach (var point in spawnZones)
            point.Initialize();
    }

    private void ActivateBots(bool active)
    {
        foreach (var bot in _botViews)
            bot.gameObject.SetActive(active);
    }

    private void InitializeBots(List<PointSpawnZone> spawnZones)
    {
        foreach (BotController botController in _botControllers)
        {
            botController.gameObject.SetActive(true);
            botController.ActivateForRace();

            if (spawnZones != null)
                botController.SetStartZone(spawnZones[0]);
            else
            {
                Debug.LogError("Нет доступной зоны спавна для бота на активной сцене.");
                botController.gameObject.SetActive(false); 
            }
        }
    }

    private void InitTriggerZones(List<PointSpawnZone> spawnZones, List<TriggerZone> triggerZones)
    {
        for (int i = 0; i < triggerZones.Count; i++)
        {
            if (i + 1 < spawnZones.Count)
                triggerZones[i].SetNextZone(spawnZones[i + 1]);
            else
                Debug.LogWarning($"Триггерная зона {i} не имеет соответствующей зоны возрождения.");
        }
    }

    private void DeactivateFlags(List<FlagPoint> flagPoints)
    {
        foreach (var flag in flagPoints)
            flag.ResetFlag();
    }

    private void DeactivateLevel()
    {
        ActivateBots(false);

        foreach (var botController in _botControllers)
            botController.gameObject.SetActive(false);

        _levelsScene[_currentLevelIndex].gameObject.SetActive(false);
        _levelsScene[_currentLevelIndex].SetBusy(false);
    }
}

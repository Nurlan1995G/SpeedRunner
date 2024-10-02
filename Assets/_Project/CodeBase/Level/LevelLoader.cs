using Assets._Project.CodeBase.Infrastracture;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private List<LevelScene> _levelsScene;

    private LevelScene _currentScene;
    private TimerLevel _timerLevel;
    private List<BotController> _botControllers;
    private List<BotView> _botViews;
    private CountdownController _countdownController;
    private Player _player;
    private NavMeshSurface _navMeshSurface;
    private RaceManager _raceManager;

    private int _currentLevelIndex;

    public void Construct(TimerLevel timerLevel, List<BotController> botControllers, List<BotView> botViews,
    CountdownController countdownController, Player player, NavMeshSurface navMeshSurface, RaceManager raceManager)
    {
        _timerLevel = timerLevel;
        _botControllers = botControllers;
        _botViews = botViews;
        _countdownController = countdownController;
        _player = player;
        _navMeshSurface = navMeshSurface;
        _raceManager = raceManager;
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

    public void DeactivateFlags()
    {
        foreach (var flag in _currentScene.FlagPoints)
            flag.ResetFlag();
    }

    public void DeactivateCoins()
    {
        foreach (var coin in _currentScene.Coins)
            coin.ActivateCoin(true);
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
        ActivateScene(_currentLevelIndex);
    }

    private void SetupLevel()
    {
        _currentScene = _levelsScene[_currentLevelIndex];
        List<PointSpawnZone> pointSpawnZones = _currentScene.PointSpawnZones;
        List<TriggerZone> triggerZones = _currentScene.TriggerZones;

        Debug.Log("начало активации");

        _player.ActivateForRace();

        InitSpawnZones(pointSpawnZones);
        //ActivateAgentBots(true);
        InitializeControllerBots(pointSpawnZones);
        InitTriggerZones(pointSpawnZones, triggerZones);
        DeactivateFlags();
        DeactivateCoins();

        _countdownController.ResetBarrier();
        _countdownController.ActivateStart();
        _raceManager.RemoveFinisher();
    }

    private void ActivateScene(int levelIndex)
    {
        for (int i = 0; i < _levelsScene.Count; i++)
        {
            _levelsScene[i].gameObject.SetActive(i == levelIndex);
            _levelsScene[i].SetBusy(i == levelIndex);
        }

        StartCoroutine(BuildNavMeshAndActivate());
    }

    private IEnumerator BuildNavMeshAndActivate()
    {
        _navMeshSurface.BuildNavMesh();
        Debug.Log("запекание произошло");

        yield return new WaitForSeconds(1f);
        Debug.Log("прошло 1 сек");

        SetupLevel();
    }

    private void InitSpawnZones(List<PointSpawnZone> spawnZones)
    {
        foreach (var point in spawnZones)
            point.Initialize();
    }

    private void ActivateAgentBots(bool active)
    {
        foreach (BotView bot in _botViews)
        {
            bot.Respawn();
            bot.gameObject.SetActive(active);
        }
    }

    private void InitializeControllerBots(List<PointSpawnZone> spawnZones)
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

    private void DeactivateLevel()
    {
        ActivateAgentBots(false);

        foreach (var botController in _botControllers)
            botController.gameObject.SetActive(false);

        _levelsScene[_currentLevelIndex].gameObject.SetActive(false);
        _levelsScene[_currentLevelIndex].SetBusy(false);
    }
}

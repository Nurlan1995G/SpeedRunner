using Assets._Project.CodeBase.Infrastracture;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private List<LevelScene> _levelsScene;

    private TimerLevel _timerLevel;
    private List<BotController> _botControllers;
    private CountdownController _countdownController;
    private Player _player;
    private NavMeshSurface _navMeshSurface;

    public void Construct(TimerLevel timerLevel, List<BotController> botControllers,
        CountdownController countdownController, Player player, NavMeshSurface navMeshSurface)
    {
        Debug.Log("Construct - LevelLoader");

        _timerLevel = timerLevel;
        _botControllers = botControllers;
        _countdownController = countdownController;
        _player = player;
        _navMeshSurface = navMeshSurface;
        
        _timerLevel.ChangeLevel += OnChangeLevel;
    }

    private void OnDisable()
    {
        _timerLevel.ChangeLevel -= OnChangeLevel;
    }

    public void StartLevelSequence()
    {
        OnChangeLevel();
    }

    private void OnChangeLevel()
    {
        Debug.Log("OnChangeLevel - LevelLoader");

        int randomLevel = RandomLevel();

        for (int i = 0; i < _levelsScene.Count; i++)
        {
            _levelsScene[i].gameObject.SetActive(i == randomLevel);
        }

        _navMeshSurface.BuildNavMesh();

        LevelScene activeLevel = _levelsScene[randomLevel];
        List<PointSpawnZone> pointSpawnZones = activeLevel.PointSpawnZones;
        List<TriggerZone> triggerZones = activeLevel.TriggerZones;

        _player.ActivateForRace();
        InitPointSpawnZones(pointSpawnZones);
        InitializeBots(pointSpawnZones);

        InitTriggerZone(pointSpawnZones, triggerZones);

        _countdownController.ResetBarrier();
        _countdownController.ActivateStart();
    }

    private void InitPointSpawnZones(List<PointSpawnZone> pointSpawnZones)
    {
        foreach (var point in pointSpawnZones)
        {
            Debug.Log("InitPointSpawnZones - point.Initialize");
            point.Initialize();
        }
    }

    private void InitializeBots(List<PointSpawnZone> pointSpawnZones)
    {
        foreach (var botController in _botControllers)
        {
            botController.ActivateForRace();
            botController.SetZone(pointSpawnZones[0]);
        }
    }

    private int RandomLevel() =>
        Random.Range(0, _levelsScene.Count);

    private void InitTriggerZone(List<PointSpawnZone> pointSpawnZones, List<TriggerZone> triggerZones)
    {
        for (int i = 0; i < triggerZones.Count; i++)
        {
            var currentTriggerZone = triggerZones[i];

            if (i + 1 < pointSpawnZones.Count)
            {
                var correspondingPointSpawnZone = pointSpawnZones[i + 1];
                currentTriggerZone.SetNextZone(correspondingPointSpawnZone);
            }
            else
                Debug.Log($"Триггерная зона {i} не имеет соответствующей зоны возрождения.");
        }
    }
}

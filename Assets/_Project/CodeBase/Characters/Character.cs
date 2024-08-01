using Assets._Project.CodeBase.Slime.Interface;
using Assets.Project.CodeBase.SharkEnemy;
using System;
using UnityEngine;

public abstract class Character : MonoBehaviour, IDestroyableSlime
{
    [SerializeField] protected GameObject SharkSkin;
    [SerializeField] protected NickName NickName;
    [SerializeField] protected CapsuleCollider CapsuleCollider;
    [SerializeField] private GameObject _crown;

    private TopCharacterManager _topCharacterManager;

    protected int Score = 1;

    public int ScoreLevel => Score;

    public event Action OnScoreChanged;

    private void Start() =>
        _topCharacterManager.RegisterShark(this);

    private void OnDestroy() =>
        _topCharacterManager.UnregisterShark(this);

    public void Init(TopCharacterManager topSharksManager) =>
        _topCharacterManager = topSharksManager;

    public abstract string GetSharkName();

    public void Destroy() =>
        Destroyable();

    public void SetCrown(bool isActive) =>
        _crown.SetActive(isActive);

    public void AddScore(int score)
    {
        Score += score;
        OnScoreChanged?.Invoke();
    }

    public virtual void Destroyable() { }
}

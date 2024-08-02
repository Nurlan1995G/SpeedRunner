using Assets._Project.CodeBase.Slime.Interface;
using System;
using UnityEngine;

public abstract class Character : MonoBehaviour, IDestroyableSlime
{
    protected int Score = 1;

    public int ScoreLevel => Score;

    public event Action OnScoreChanged;

    private void Update()
    {
    }


    public abstract string GetSharkName();

    public void Destroy() =>
        Destroyable();

    public void AddScore(int score)
    {
        Score += score;
        OnScoreChanged?.Invoke();
    }

    public virtual void Destroyable() { }
}
using System;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public static ScoreSystem Instance { get; private set; }
    public event EventHandler OnScoreChanged;

    [SerializeField] private int startingScore = 0;
    private int _score;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError("There are multiple instances of ScoreSystem.");
            return;
        }

        Instance = this;
        _score = startingScore;
    }

    public int GetScore()
    {
        return _score;
    }

    public void AddPoints(int pointsToAdd)
    {
        if (pointsToAdd <= 0)
        {
            return;
        }

        _score += pointsToAdd;
        OnScoreChanged?.Invoke(this, EventArgs.Empty);
    }

    public void SetScore(int newScore)
    {
        _score = Mathf.Max(0, newScore);
        OnScoreChanged?.Invoke(this, EventArgs.Empty);
    }

    [ContextMenu("Add 100 Score (Test)")]
    private void Add100ScoreForTest()
    {
        AddPoints(100);
    }

    public void ResetScore()
    {
        _score = startingScore;
        OnScoreChanged?.Invoke(this, EventArgs.Empty);
    }
}

using System;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    private const int PointsPerWorm = 100;

    public static ScoreSystem Instance { get; private set; }
    public event EventHandler OnScoreChanged;

    [SerializeField] private int startingScore = 0;
    [SerializeField] private int testWormCount = 10;
    private int _score;
    private int _caughtWormCount;
    private int _totalWormCount;
    private int _remainingWormCount;

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

    public int GetCaughtWormCount()
    {
        return _caughtWormCount;
    }

    public int GetTotalWormCount()
    {
        return _totalWormCount;
    }

    public int GetRemainingWormCount()
    {
        return _remainingWormCount;
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

    public void RegisterCaughtWorm()
    {
        if (_remainingWormCount <= 0)
        {
            return;
        }

        _caughtWormCount++;
        _remainingWormCount = Mathf.Max(0, _remainingWormCount - 1);
    }

    public void SetWormCount(int totalWormCount)
    {
        _totalWormCount = Mathf.Max(0, totalWormCount);
        _remainingWormCount = _totalWormCount;
        _caughtWormCount = 0;
        _score = startingScore;
        OnScoreChanged?.Invoke(this, EventArgs.Empty);
    }

    [ContextMenu("Test/Set Worm Count (Resets Score)")]
    private void SetWormCountForTest()
    {
        SetWormCount(testWormCount);
    }

    [ContextMenu("Test/Catch One Worm")]
    private void CatchOneWormForTest()
    {
        if (_totalWormCount <= 0)
        {
            SetWormCount(testWormCount);
        }

        if (_remainingWormCount <= 0)
        {
            return;
        }

        RegisterCaughtWorm();
        AddPoints(PointsPerWorm);
        Debug.Log("Test catch: score=" + _score + ", caught=" + _caughtWormCount + ", remaining=" + _remainingWormCount + ", total=" + _totalWormCount);
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
        _caughtWormCount = 0;
        _remainingWormCount = _totalWormCount;
        OnScoreChanged?.Invoke(this, EventArgs.Empty);
    }
}

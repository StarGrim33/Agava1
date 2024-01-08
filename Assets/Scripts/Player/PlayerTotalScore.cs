using System;
using UnityEngine;

public class PlayerTotalScore : MonoBehaviour
{
    private int _playerTotalScore;

    public event Action ScoreChanged;

    public int TotalScore => _playerTotalScore;

    private void Awake()
    {
        LoadTotalScore();
    }

    public void SaveTotalScore(int value = 0)
    {
        _playerTotalScore += value;
        PlayerPrefs.SetInt(Constants.TotalScoreKey, _playerTotalScore);
        PlayerPrefs.Save();
    }

    public void LoadTotalScore()
    {
        if (PlayerPrefs.HasKey(Constants.TotalScoreKey))
        {
            _playerTotalScore = PlayerPrefs.GetInt(Constants.TotalScoreKey);
        }
    }

    public void ReduceScore(int value)
    {
        if (_playerTotalScore > 0)
        {
            _playerTotalScore -= value;
            ScoreChanged?.Invoke();
            SaveTotalScore();
        }
    }

    public void AddScore(int value)
    {
        _playerTotalScore += value;
    }
}

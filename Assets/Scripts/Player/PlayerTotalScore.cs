using UnityEngine;

public class PlayerTotalScore : MonoBehaviour
{
    public const string TotalScoreKey = "TotalScore";

    public int TotalScore => _playerTotalScore;

    private int _playerTotalScore;

    private void Awake()
    {
        LoadTotalScore();
    }

    public void SaveTotalScore(int value)
    {
        _playerTotalScore += value;
        PlayerPrefs.SetInt(TotalScoreKey, _playerTotalScore);
        Debug.Log($"—чет: {_playerTotalScore}");
        PlayerPrefs.Save();
    }

    public void LoadTotalScore()
    {
        if (PlayerPrefs.HasKey(TotalScoreKey))
        {
            _playerTotalScore = PlayerPrefs.GetInt(TotalScoreKey);
            Debug.Log($"—чет: {_playerTotalScore}");
        }
    }

    public void ReduceScore(int value)
    {
        if (_playerTotalScore > 0)
        {
            _playerTotalScore -= value;
        }
    }

    public void AddScore(int value)
    {
        _playerTotalScore += value;
    }
}

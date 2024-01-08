using Agava.YandexGames;
using System;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private GateSpawner _gateSpawner;
    [SerializeField] private PlayerTotalScore _totalScore;
    [SerializeField] private int _scoreForWin;
    private int _playerScore = 0;
    private int _enemyScore = 0;
    private int _scorePerGoal = 10;

    public event Action OnPlayerScoreChanged;
    public event Action OnEnemyScoreChanged;

    public int ScoreForWin => _scoreForWin;

    public int PlayerScore { get; private set; } = 0;

    public int EnemyScore { get; private set; } = 0;

    private void OnEnable()
    {
        _gateSpawner.OnGoalGateSpawned += OnGoalGateSpawned;
        _totalScore.LoadTotalScore();
    }

    private void OnDisable() => _gateSpawner.OnGoalGateSpawned -= OnGoalGateSpawned;

    public void UpdateLeaderboardScore()
    {
        Leaderboard.GetPlayerEntry(Constants.LeaderboardName, (result) =>
        {
            if (result == null)
            {
                Leaderboard.SetScore(Constants.LeaderboardName, _totalScore.TotalScore);
            }
            else
            {
                if (result.score < _totalScore.TotalScore)
                {
                    Leaderboard.SetScore(Constants.LeaderboardName, _totalScore.TotalScore);
                }
            }
        });
    }

    public void DoubleScoreForAD()
    {
        int doubleMultiply = 2;
        PlayerScore *= doubleMultiply;
        _totalScore.AddScore(PlayerScore);
        _totalScore.SaveTotalScore();
    }

    private void OnGoalGateSpawned(Gate gate) => gate.OnGoalScored += OnGoalScored;

    private void OnGoalScored(Gate gate, bool isEnemyGoal)
    {
        if (isEnemyGoal)
        {
            EnemyScore += _scorePerGoal;
            OnEnemyScoreChanged?.Invoke();
        }
        else if (isEnemyGoal == false)
        {
            PlayerScore += _scorePerGoal;
            OnPlayerScoreChanged.Invoke();

            if (PlayerScore >= _scoreForWin)
            {
                UpdateLeaderboardScore();
                _totalScore.SaveTotalScore(PlayerScore);
            }
        }

        gate.OnGoalScored -= OnGoalScored;
    }
}

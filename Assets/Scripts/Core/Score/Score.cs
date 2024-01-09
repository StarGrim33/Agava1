using System;
using Agava.YandexGames;
using Player;
using UnityEngine;
using Utils;

namespace Core
{
    public class Score : MonoBehaviour
    {
        [SerializeField] private GateSpawner _gateSpawner;
        [SerializeField] private PlayerTotalScore _totalScore;
        [SerializeField] private int _scoreForWin;
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

        private void OnDisable()
        {
            _gateSpawner.OnGoalGateSpawned -= OnGoalGateSpawned;
        }

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
            const int doubleMultiply = 2;
            PlayerScore *= doubleMultiply;
            _totalScore.AddScore(PlayerScore);
            _totalScore.SaveTotalScore();
        }

        private void OnGoalGateSpawned(Gate gate)
        {
            gate.OnPlayerGoalScored += OnPlayerGoalScored;
            gate.OnEnemyGoalScored += OnEnemyGoalScored;
        }

        private void OnPlayerGoalScored(Gate gate)
        {
            PlayerScore += _scorePerGoal;
            OnPlayerScoreChanged?.Invoke();

            if (PlayerScore >= _scoreForWin)
            {
                UpdateLeaderboardScore();
                _totalScore.SaveTotalScore(PlayerScore);
            }
        }

        private void OnEnemyGoalScored(Gate gate)
        {
            EnemyScore += _scorePerGoal;
            OnEnemyScoreChanged?.Invoke();
        }
    }
}

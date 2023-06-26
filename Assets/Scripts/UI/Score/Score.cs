using Agava.YandexGames;
using UnityEngine;
using UnityEngine.Events;

public class Score : MonoBehaviour
{
    [SerializeField] private GateSpawner _gateSpawner;
    [SerializeField] private PlayerTotalScore _totalScore;
    [SerializeField] private int _scoreForWin;

    public event UnityAction OnPlayerScoreChanged;
    public event UnityAction OnEnemyScoreChanged;

    public int ScoreForWin
    {
        get { return _scoreForWin; }
        private set { _scoreForWin = value; }
    }

    public int PlayerScore
    {
        get { return _playerScore; }
        private set { _playerScore = value; }
    }

    public int EnemyScore
    {
        get { return _enemyScore; }
        private set { _enemyScore = value; }
    }

    private int _playerScore = 0;
    private int _enemyScore = 0;
    private int _scorePerGoal = 10;

    private void OnEnable()
    {
        _gateSpawner.OnGoalGateSpawned += OnGoalGateSpawned;
        _totalScore.LoadTotalScore();
    }

    private void OnDisable()
    {
        _gateSpawner.OnGoalGateSpawned -= OnGoalGateSpawned;
    }

    public void GetLeaderboardPlayerEntryButtonClick()
    {
        Leaderboard.GetPlayerEntry("1", (result) =>
        {
            if (result == null)
            {
                Leaderboard.SetScore("1", _totalScore.TotalScore);
            }
            else
            {
                if (result.score < _totalScore.TotalScore)
                {
                    Leaderboard.SetScore("1", _totalScore.TotalScore);
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

    private void OnGoalGateSpawned(Gate gate)
    {
        gate.OnGoalScored += OnGoalScored;
    }

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
                GetLeaderboardPlayerEntryButtonClick();
                _totalScore.SaveTotalScore(PlayerScore);
            }
        }

        gate.OnGoalScored -= OnGoalScored;
    }
}

using UnityEngine;
using UnityEngine.Events;

public class Score : MonoBehaviour
{
    public const string TotalScoreKey = "TotalScore";

    [SerializeField] private GateSpawner _gateSpawner;
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
    private int _playerTotalScore = 0;

    private void OnEnable()
    {
        _gateSpawner.OnGoalGateSpawned += OnGoalGateSpawned;
        LoadTotalScore();
    }

    private void OnDisable()
    {
        _gateSpawner.OnGoalGateSpawned -= OnGoalGateSpawned;
        SaveTotalScore();
    }

    public void DoubleScoreForAD()
    {
        _playerTotalScore *= 2;
        Debug.Log(_playerTotalScore);
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
        else
        {
            PlayerScore += _scorePerGoal;
            _playerTotalScore += _scorePerGoal;
            SaveTotalScore();
            OnPlayerScoreChanged.Invoke();
        }

        gate.OnGoalScored -= OnGoalScored;
    }

    private void SaveTotalScore()
    {
        PlayerPrefs.SetInt(TotalScoreKey, _playerTotalScore);
        Debug.Log($"—чет: {_playerTotalScore}");
        PlayerPrefs.Save();
    }

    private void LoadTotalScore()
    {
        if (PlayerPrefs.HasKey(TotalScoreKey))
        {
            _playerTotalScore = PlayerPrefs.GetInt(TotalScoreKey);
        }
    }
}

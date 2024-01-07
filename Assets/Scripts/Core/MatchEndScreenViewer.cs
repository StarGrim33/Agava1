using UnityEngine;

public class MatchEndScreenViewer : MonoBehaviour
{
    [SerializeField] private GameObject _loseScreen;
    [SerializeField] private GameObject _victoryScreen;
    [SerializeField] private Score _score;
    [SerializeField] private ScoreTextViewer _textViewer;

    private void OnEnable()
    {
        _score.OnPlayerScoreChanged += OnPlayerScoreChanged;
        _score.OnEnemyScoreChanged += OnEnemyScoreChanged;
    }

    private void OnDisable()
    {
        _score.OnPlayerScoreChanged -= OnPlayerScoreChanged;
        _score.OnEnemyScoreChanged -= OnEnemyScoreChanged;
    }

    private void OnEnemyScoreChanged()
    {
        if (_score.EnemyScore >= _score.ScoreForWin)
            ShowPanel(_loseScreen);
    }

    private void OnPlayerScoreChanged()
    {
        if (_score.PlayerScore >= _score.ScoreForWin)
            ShowPanel(_victoryScreen);
    }

    private void ShowPanel(GameObject screen)
    {
        Time.timeScale = 0f;
        screen.gameObject.SetActive(true);
        _textViewer.ShowTotalPlayerScore();
    }
}

using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreTextViewer : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerScoreText;
    [SerializeField] private TMP_Text _enemyScoreText;
    [SerializeField] private GameObject _textContainer;
    [SerializeField] private TMP_Text _goalText;
    [SerializeField] private TMP_Text _totalScore;
    [SerializeField] private GateSpawner _spawner;
    [SerializeField] private Score _score;

    private void OnEnable()
    {
        _score.OnPlayerScoreChanged += OnPlayerScoreChanged;
        _score.OnEnemyScoreChanged += OnEnemyScoreChanged;
        _enemyScoreText.text = $"{_score.EnemyScore}/{_score.ScoreForWin}";
        _playerScoreText.text = $"{_score.PlayerScore}/{_score.ScoreForWin}";
    }

    private void OnDisable()
    {
        _score.OnPlayerScoreChanged -= OnPlayerScoreChanged;
        _score.OnEnemyScoreChanged -= OnEnemyScoreChanged;
    }

    public void ShowTotalPlayerScore() => _totalScore.text = _score.PlayerScore.ToString();

    private void OnEnemyScoreChanged() => _enemyScoreText.text = $"{_score.EnemyScore}/{_score.ScoreForWin}";

    private void OnPlayerScoreChanged()
    {
        _playerScoreText.text = $"{_score.PlayerScore}/{_score.ScoreForWin}";
        _textContainer.SetActive(true);

        StartCoroutine(TextFading());
    }

    private IEnumerator TextFading()
    {
        float duration = 3f;
        float elapsedTime = 0f;

        Color startColor = _goalText.color;
        Color targetColor = new(startColor.r, startColor.g, startColor.b, 0f);

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            Color newColor = Color.Lerp(startColor, targetColor, t);
            _goalText.color = newColor;

            yield return null;
        }

        _textContainer.SetActive(false);
        _goalText.color = new Color(startColor.r, startColor.g, startColor.b, 1f);

        yield return null;
    }
}

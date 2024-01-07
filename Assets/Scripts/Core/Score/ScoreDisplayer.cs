using TMPro;
using UnityEngine;

public class ScoreDisplayer : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private PlayerTotalScore _totalScore;

    private void OnEnable() => _totalScore.ScoreChanged += OnScoreChanged;

    private void OnDisable() => _totalScore.ScoreChanged -= OnScoreChanged;

    private void Start() => UpdateCoinCountText();

    public void UpdateCoinCountText() => _text.text = _totalScore.TotalScore.ToString();

    private void OnScoreChanged() => UpdateCoinCountText();
}

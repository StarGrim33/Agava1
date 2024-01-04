using TMPro;
using UnityEngine;

public class ScoreDisplayer : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private PlayerTotalScore _totalScore;

    private void OnEnable()
    {
        _totalScore.ScoreChanged += OnScoreChanged;
    }

    private void OnDisable()
    {
        _totalScore.ScoreChanged -= OnScoreChanged;
    }

    public void UpdateCoinCountText()
    {
        _text.text = _totalScore.TotalScore.ToString();
    }

    private void OnScoreChanged()
    {
        UpdateCoinCountText();
    }

    private void Start()
    {
        UpdateCoinCountText();
    }
}

using TMPro;
using UnityEngine;

public class ScoreDisplayer : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    private int _score;

    private void Start()
    {
        LoadCoinCount();
        UpdateCoinCountText();
    }

    private void UpdateCoinCountText()
    {
        _text.text = _score.ToString();
    }

    private void LoadCoinCount()
    {
        if (PlayerPrefs.HasKey(Score.TotalScoreKey))
        {
            _score = PlayerPrefs.GetInt(Score.TotalScoreKey);
            _text.text = _score.ToString();
        }
    }
}

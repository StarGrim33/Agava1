using Agava.YandexGames;
using TMPro;
using UnityEngine;

public class ScoreDisplayer : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private PlayerTotalScore _totalScore;

    private void Start()
    {
        UpdateCoinCountText();
    }

    public void UpdateCoinCountText()
    {
        _text.text = _totalScore.TotalScore.ToString();
        Debug.Log(_text.text);
    }
}

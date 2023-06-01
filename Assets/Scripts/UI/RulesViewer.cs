using System.Collections;
using TMPro;
using UnityEngine;

public class RulesViewer : MonoBehaviour
{
    [SerializeField] private GameObject _levelRules;
    [SerializeField] private string _additionalRuleText;
    [SerializeField] private float _delay;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Score _score;

    private void Start()
    {
        StartCoroutine(ShowRules());
    }

    private IEnumerator ShowRules()
    {
        var waitForSeconds = new WaitForSeconds(_delay);

        yield return waitForSeconds;

        SetScoreRule();
        _levelRules.SetActive(true);


        _levelRules.SetActive(false);
    }

    private void SetScoreRule()
    {
        var score = _score.ScoreForWin;
        var rule = $"Набери первым {score} очков {_additionalRuleText}";
        _text.text = rule.ToString();
    }
}

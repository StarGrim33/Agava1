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

        Invoke(nameof(SetScoreRule), 1f);

        yield return waitForSeconds;

        SetScoreRule();

        _levelRules.SetActive(false);
    }

    private void SetScoreRule()
    {
        _levelRules.SetActive(true);
    }
}

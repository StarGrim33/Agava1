using System.Collections;
using UnityEngine;

public class RulesViewer : MonoBehaviour
{
    [SerializeField] private GameObject _levelRules;
    [SerializeField] private float _delay;

    private void Start()
    {
        StartCoroutine(ShowRules());
    }

    private IEnumerator ShowRules()
    {
        var waitForSeconds = new WaitForSeconds(_delay);

        _levelRules.SetActive(true);

        yield return waitForSeconds;

        _levelRules.SetActive(false);
    }
}

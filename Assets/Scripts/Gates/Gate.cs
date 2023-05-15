using System.Collections;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private Goal _goal;

    private void OnEnable()
    {
        _goal.OnGoalScored += OnGoalScored;
    }

    private void OnGoalScored()
    {
        StartCoroutine(Destoyer());
    }

    private void OnDisable()
    {
        _goal.OnGoalScored -= OnGoalScored;
    }

    private IEnumerator Destoyer()
    {
        var waitForSeconds = new WaitForSeconds(2f);
        yield return waitForSeconds;
        Destroy(gameObject);
    }
}

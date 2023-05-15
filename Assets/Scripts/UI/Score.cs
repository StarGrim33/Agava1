using System.Collections;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private GateSpawner _gateSpawner;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private GameObject _textContainer;
    [SerializeField] private TMP_Text _goalText;

    private int _score;
    private int _scorePerGoal = 10;

    private void OnEnable()
    {
        _gateSpawner.OnGoalGateSpawned += OnGoalGateSpawned;
    }

    private void OnDisable()
    {
        _gateSpawner.OnGoalGateSpawned -= OnGoalGateSpawned;
    }

    private void OnGoalGateSpawned(Gate gate)
    {
        gate.OnGoalScored += OnGoalScored;
    }

    private void OnGoalScored(Gate gate)
    {
        _score += _scorePerGoal;
        _scoreText.text = _score.ToString();
        _textContainer.SetActive(true);
        gate.OnGoalScored -= OnGoalScored;
        StartCoroutine(TextFading());
    }

    private IEnumerator TextFading()
    {
        float duration = 3f;
        float elapsedTime = 0f;

        Color startColor = _goalText.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

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

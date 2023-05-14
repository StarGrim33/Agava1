using System.Collections;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private Goal _goal;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private GameObject _textContainer;
    [SerializeField] private TMP_Text _goalText;
    [SerializeField] private ParticleSystem _particleSystem;

    private int _score;
    private int _scorePerGoal = 10;

    private void OnEnable()
    {
        _goal.OnGoalScored += OnGoalScored;
    }

    private void OnDisable()
    {
        _goal.OnGoalScored -= OnGoalScored;
    }

    private void OnGoalScored()
    {
        _score += _scorePerGoal;
        _scoreText.text = _score.ToString();
        _textContainer.SetActive(true);
        _particleSystem.Play();
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

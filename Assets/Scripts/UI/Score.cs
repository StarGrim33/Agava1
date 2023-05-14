using System.Collections;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private Goal _goal;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private GameObject _goalText;
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
        string text = "Твой счет: ";
        _score += _scorePerGoal;
        _text.text = text + _score.ToString();
        _goalText.SetActive(true);
        _particleSystem.Play();
        StartCoroutine(TextFading());
    }

    private IEnumerator TextFading()
    {
        float duration = 3f;
        float elapsedTime = 0f;

        Color originalColor = _text.color;

        while(elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            Color newColor = originalColor;
            newColor.a = alpha;
            _text.color = newColor;

            yield return null;
        }

        _goalText.SetActive(false);
        _text.color = originalColor;
    }
}

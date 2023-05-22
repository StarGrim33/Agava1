using UnityEngine;

public class GoalSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Score _score;

    private void OnEnable()
    {
        _score.OnPlayerScoreChanged += OnPlayerScoreChanged;
    }

    private void OnDisable()
    {
        _score.OnPlayerScoreChanged -= OnPlayerScoreChanged;
    }

    private void OnPlayerScoreChanged()
    {
        _audioSource.Play();   
    }
}

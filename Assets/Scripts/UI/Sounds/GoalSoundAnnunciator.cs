using UnityEngine;

public class GoalSoundAnnunciator : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Score _score;
    [SerializeField] private AudioClip _goalForPlayer;
    [SerializeField] private AudioClip _goalForEnemy;
    [SerializeField] private AudioSource _mainSound;

    private void OnEnable()
    {
        _score.OnPlayerScoreChanged += OnPlayerScoreChanged;
        _score.OnEnemyScoreChanged += OnEnemyScoreChanged;
    }

    private void OnDisable()
    {
        _score.OnPlayerScoreChanged -= OnPlayerScoreChanged;
        _score.OnEnemyScoreChanged -= OnEnemyScoreChanged;
    }

    private void OnPlayerScoreChanged()
    {
        if (_mainSound.isPlaying)
            _audioSource.PlayOneShot(_goalForPlayer);
    }

    private void OnEnemyScoreChanged()
    {
        if (_mainSound.isPlaying)
            _audioSource.PlayOneShot(_goalForEnemy);
    }
}

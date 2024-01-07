using UnityEngine;

public class GoalSoundAnnunciator : MonoBehaviour, IGoalSoundAnnunciator
{
    [SerializeField] private AudioYB _audioSource;
    [SerializeField] private Score _score;
    [SerializeField] private AudioSource _mainSound;
    [SerializeField] private string _goalForPlayer;
    [SerializeField] private string _goalForEnemy;

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

    public void Annunciate(string soundName)
    {
        if (_mainSound.isPlaying)
            _audioSource.PlayOneShot(soundName);
    }

    private void OnPlayerScoreChanged()
    {
        Annunciate(_goalForPlayer);
    }

    private void OnEnemyScoreChanged()
    {
        Annunciate(_goalForEnemy);
    }
}

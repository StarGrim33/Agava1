using UnityEngine;
using UnityEngine.Events;

public class Gate : MonoBehaviour
{
    public event UnityAction<Gate, bool> OnGoalScored;

    [SerializeField] private ParticleSystem _particleSystem;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerBall>(out PlayerBall playerBall))
        {
            ScoreGoal(false);
        }

        if(other.TryGetComponent<EnemyBall>(out EnemyBall enemyBall))
        {
            Debug.Log("Enemy Goal");
            ScoreGoal(true);
        }
    }

    private void ScoreGoal(bool isEnemyGoal)
    {
        _particleSystem.Play();
        OnGoalScored?.Invoke(this, isEnemyGoal);
        Destroy(gameObject, 1f);
    }
}

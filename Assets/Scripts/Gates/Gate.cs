using UnityEngine;
using UnityEngine.Events;

public class Gate : MonoBehaviour
{
    public event UnityAction<Gate, bool> OnGoalScored;

    [SerializeField] private ParticleSystem[] _particleSystem;
    [SerializeField] private Transform _middleTarget;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerBall>(out PlayerBall playerBall))
        {
            ScoreGoal(false);
        }

        if(other.TryGetComponent<EnemyBall>(out EnemyBall enemyBall))
        {
            ScoreGoal(true);
        }
    }

    public Vector3 MiddleTarget()
    {
        Vector3 target = _middleTarget.position;
        return target;
    }

    private void ScoreGoal(bool isEnemyGoal)
    {
        ParticleSystem particle = GetRandomParticle();
        particle.Play();
        OnGoalScored?.Invoke(this, isEnemyGoal);
        Destroy(gameObject, 1f);
    }

    private ParticleSystem GetRandomParticle()
    {
        int randomNumber = Random.Range(0, _particleSystem.Length);
        return _particleSystem[randomNumber];
    }
}

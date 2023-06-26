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
            PlayerGoal();

        if (other.TryGetComponent<EnemyBall>(out EnemyBall enemyBall))
            EnemyGoal();
    }

    public Vector3 MiddleTarget()
    {
        Vector3 target = _middleTarget.position;
        return target;
    }

    private void PlayerGoal()
    {
        Goal();
        OnGoalScored?.Invoke(this, false);
    }

    private void EnemyGoal()
    {
        Goal();
        OnGoalScored?.Invoke(this, true);
    }

    private ParticleSystem GetRandomParticle()
    {
        int randomNumber = Random.Range(0, _particleSystem.Length);
        return _particleSystem[randomNumber];
    }

    private void Goal()
    {
        float destroyDelay = 1f;
        ParticleSystem particle = GetRandomParticle();
        particle.Play();
        Destroy(gameObject, destroyDelay);
    }
}

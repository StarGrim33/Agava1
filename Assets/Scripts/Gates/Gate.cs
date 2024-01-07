using System;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _particleSystem;
    [SerializeField] private Transform _middleTarget;

    public event Action<Gate, bool> OnGoalScored;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerBall>(out _))
            PlayerGoal();

        if (other.TryGetComponent<EnemyBall>(out _))
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
        int randomNumber = UnityEngine.Random.Range(0, _particleSystem.Length);
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

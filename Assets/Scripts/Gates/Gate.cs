using System;
using Ball;
using UnityEngine;

namespace Core
{
    public class Gate : MonoBehaviour
    {
        [SerializeField] private ParticleSystem[] _particleSystem;
        [SerializeField] private Transform _middleTarget;

        public event Action<Gate> OnPlayerGoalScored;

        public event Action<Gate> OnEnemyGoalScored;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerBall>(out _))
            {
                PlayerGoal();
            }
            else if (other.TryGetComponent<EnemyBall>(out _))
            {
                EnemyGoal();
            }
        }

        public Vector3 DetermineMiddlePosition()
        {
            Vector3 target = _middleTarget.position;
            return target;
        }

        private void PlayerGoal()
        {
            Goal();
            OnPlayerGoalScored?.Invoke(this);
        }

        private void EnemyGoal()
        {
            Goal();
            OnEnemyGoalScored?.Invoke(this);
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
}

using System.Collections;
using Ball;
using Core;
using Player;
using UnityEngine;
using Utils;

namespace Enemy
{
    public class EnemyKickingBall : KickingBall
    {
        [SerializeField] private AnimatorEnemyPlayer _enemyAnimator;
        [SerializeField] private GateSpawner _gateSpawner;
        [SerializeField] private BaseBall _ball;
        [SerializeField] private float _missProbability;
        [SerializeField] private int _hits;
        private WaitForSeconds _waitForSeconds;
        private WaitForSeconds _reloadWaitForSeconds;
        private Vector3 _goalPosition;
        private bool _isKicking = false;
        private bool _isKickingCoroutineRunning = false;
        private float _delayBeforeHit = 4f;

        public bool IsKicking => _isKicking;

        private void OnEnable()
        {
            _gateSpawner.OnGateSpawned += OnGateSpawned;
            _enemyAnimator.OnKickedEnemyAnimation += OnKickedAnimationFinished;
        }

        private void OnDisable()
        {
            _gateSpawner.OnGateSpawned -= OnGateSpawned;
            _enemyAnimator.OnKickedEnemyAnimation -= OnKickedAnimationFinished;
        }

        protected override void Start()
        {
            _waitForSeconds = new WaitForSeconds(_delayBeforeHit);
            _reloadWaitForSeconds = new WaitForSeconds(TimeHitsReload);
            base.Start();
        }

        private void Update()
        {
            if (HitsRemained > 0 && !_isKickingCoroutineRunning)
                StartCoroutine(Kicking());
        }

        public void ChangeMissProbability(float value)
        {
            if (value > 0 && value < 1)
                _missProbability = value;
        }

        protected override IEnumerator Kicking()
        {
            _isKickingCoroutineRunning = true;

            while (HitsRemained > 0 && _goalPosition != null)
            {
                if (!_isKicking)
                {
                    HitDirection = DetermineEnemyKickDirection();
                    Animator.SetBool(Constants.IsAiming, true);
                    Animator.Play(AnimatorEnemyPlayer.States.Strike, 0, 0);
                    _isKicking = true;

                    yield return _waitForSeconds;

                    _isKicking = false;
                }

                yield return null;
            }

            Animator.SetBool(Constants.IsAiming, false);
            _isKickingCoroutineRunning = false;
        }

        protected override void OnKickedAnimationFinished()
        {
            ParticleSystem.Play();
            BallRigidbody.velocity = Vector3.zero;
            BallRigidbody.AddForce(HitDirection * HifForce, ForceMode.Impulse);

            if (HitsRemained > 0)
                HitsRemained--;

            if (HitsRemained <= 0)
                StartCoroutine(ReloadHits());

            _isKicking = false;
        }

        protected override IEnumerator ReloadHits()
        {
            if (HitsRemained <= 0)
            {
                yield return _reloadWaitForSeconds;
                HitsRemained = _hits;
            }
        }

        private Vector3 DetermineEnemyKickDirection()
        {
            if (IsKickMissed() == false)
            {
                Vector3 kickDirection = _goalPosition - _ball.transform.position;
                kickDirection.y = 0f;
                return kickDirection.normalized;
            }
            else if (IsKickMissed())
            {
                Vector2 randomPoint = Random.insideUnitCircle.normalized;
                Vector3 kickDirection = new (randomPoint.x, 0f, randomPoint.y);
                return kickDirection.normalized;
            }

            return _goalPosition;
        }

        private void OnGateSpawned(Vector3 position)
        {
            _goalPosition = position;
        }

        private bool IsKickMissed()
        {
            return Random.value < _missProbability;
        }
    }
}

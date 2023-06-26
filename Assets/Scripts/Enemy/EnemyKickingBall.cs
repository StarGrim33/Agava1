using System.Collections;
using UnityEngine;

public class EnemyKickingBall : KickingBall
{
    [SerializeField] private AnimatorEnemyPlayer _enemyAnimator;
    [SerializeField] private GateSpawner _gateSpawner;
    [SerializeField] private Ball _ball;
    [SerializeField] private float _missProbability;
    [SerializeField] private int _hits;

    public bool IsKicking => _isKicking;

    private bool _isKicking = false;
    private Vector3 _goalPosition;
    private bool _isKickingCoroutineRunning = false;

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

    private void Update()
    {
        if (_hitsRemained > 0 && !_isKickingCoroutineRunning)
            StartCoroutine(Kicking());
    }

    public void ChangeMissProbability(float value)
    {
        if(value > 0 && value < 1)
            _missProbability = value;
    }

    protected override IEnumerator Kicking()
    {
        _isKickingCoroutineRunning = true;

        yield return new WaitForSeconds(3f);

        while (_hitsRemained > 0 && _goalPosition != null)
        {
            if (!_isKicking)
            {
                _hitDirection = CalculateEnemyHitDirection();                
                _animator.SetBool(Constants.IsAiming, true);
                _animator.Play(AnimatorEnemyPlayer.States.Strike, 0, 0f);
                _isKicking = true;

                yield return new WaitForSeconds(4f);
                _isKicking = false;
            }

            yield return null;
        }

        _animator.SetBool(Constants.IsAiming, false);
        _isKickingCoroutineRunning = false;
    }

    protected override void OnKickedAnimationFinished()
    {
        _particleSystem.Play();
        _ballRigidbody.velocity = Vector3.zero;
        _ballRigidbody.AddForce(_hitDirection * _hifForce, ForceMode.Impulse);

        
        if (_hitsRemained > 0)
            _hitsRemained--;

        if (_hitsRemained <= 0)
            StartCoroutine(ReloadHits());

        _isKicking = false;
    }

    protected override IEnumerator ReloadHits()
    {
        var waitForSeconds = new WaitForSeconds(_timeHitsReload);

        if (_hitsRemained <= 0)
        {
            yield return waitForSeconds;
            _hitsRemained = _hits;
        }
    }

    private Vector3 CalculateEnemyHitDirection()
    {
        if(IsKickMissed() == false)
        {
            Vector3 kickDirection = _goalPosition - _ball.transform.position;
            kickDirection.y = 0f;
            return kickDirection.normalized;
        }
        else if(IsKickMissed()) 
        {
            Vector2 randomPoint = Random.insideUnitCircle.normalized;
            Vector3 kickDirection = new Vector3(randomPoint.x, 0f, randomPoint.y);
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
        if(Random.value < _missProbability) 
            return true;

        return false;
    }
}

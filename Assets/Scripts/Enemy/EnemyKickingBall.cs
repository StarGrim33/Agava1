using System.Collections;
using UnityEngine;

public class EnemyKickingBall : KickingBall
{
    [SerializeField] private AnimatorEnemyPlayer _enemyAnimator;
    [SerializeField] private GateSpawner _gateSpawner;
    [SerializeField] private Ball _ball;

    public bool IsKicking => _isKicking;

    private bool _isKicking = false;
    private Vector3 _goalPosition;
    private bool _isKickingCoroutineRunning = false;

    private void OnEnable()
    {
        _gateSpawner.OnGateSpawned += OnGateSpawned;
        _enemyAnimator.OnKickedEnemyAnimation += OnKickedAnimationFinished;
    }

    private void OnGateSpawned(Vector3 position)
    {
        _goalPosition = position;
    }

    private void OnDisable()
    {
        _gateSpawner.OnGateSpawned -= OnGateSpawned;
        _enemyAnimator.OnKickedEnemyAnimation -= OnKickedAnimationFinished;
    }

    protected override void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_hitsRemained > 0 && !_isKickingCoroutineRunning)
            StartCoroutine(Kicking());
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
                _animator.SetBool(AnimatorEnemyPlayer.Params.IsAiming, true);
                _animator.Play(AnimatorEnemyPlayer.States.Strike, 0, 0f);
                _isKicking = true;

                yield return new WaitForSeconds(3f);
                _isKicking = false;
            }

            yield return null;
        }

        _animator.SetBool(AnimatorEnemyPlayer.Params.IsAiming, false);
        _isKickingCoroutineRunning = false;
    }

    protected override void OnKickedAnimationFinished()
    {
        _particleSystem.Play();
        _ballRigidbody.AddForce(_hitDirection * _hifForce, ForceMode.Impulse);

        if (_hitsRemained > 0)
        {
            _hitsRemained--;
        }

        if (_hitsRemained <= 0)
            StartCoroutine(ReloadHits());

        _isKicking = false;
    }

    private Vector3 CalculateEnemyHitDirection()
    {
        Debug.Log(_goalPosition);
        Vector3 kickDirection = _goalPosition - _ball.transform.position;
        kickDirection.y = 0f;
        return kickDirection.normalized;
    }
}

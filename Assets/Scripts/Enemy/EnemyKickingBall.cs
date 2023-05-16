using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyKickingBall : KickingBall
{
    [SerializeField] private AnimatorEnemyPlayer _enemyAnimator;
    [SerializeField] private GateSpawner _gateSpawner;

    public bool IsKicking => _isKicking;

    private bool _isKicking = false;
    private Vector3 _goalPosition;

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
        StartCoroutine(Kicking());
    }

    protected override IEnumerator Kicking()
    {
        yield return new WaitForSeconds(2f);

        while (_hitsRemained > 0)
        {
            if (!_isKicking)
            {
                Vector3 enemyHitDirection = CalculateEnemyHitDirection();

                _fooballPlayer.transform.rotation = Quaternion.LookRotation(enemyHitDirection);
                _hitDirection = enemyHitDirection;

                _animator.SetBool(AnimatorEnemyPlayer.Params.IsAiming, true);
                _animator.Play(AnimatorEnemyPlayer.States.Strike, 0, 0f);
                _isKicking = true;

                yield return new WaitForSeconds(2f);
                _isKicking = false;
            }

            yield return null;
        }

        _animator.SetBool(AnimatorEnemyPlayer.Params.IsAiming, false);
    }

    protected override void OnKickedAnimationFinished()
    {
        base.OnKickedAnimationFinished();
        _isKicking = false;
    }

    private Vector3 CalculateEnemyHitDirection()
    {
        Vector3 kickDirection = _goalPosition - transform.position;
        kickDirection.y = 0f;
        return kickDirection.normalized;
    }
}

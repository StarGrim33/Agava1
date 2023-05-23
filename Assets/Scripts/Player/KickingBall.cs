using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public abstract class KickingBall : MonoBehaviour
{
    [SerializeField] protected Rigidbody _ballRigidbody;
    [SerializeField] protected Transform _ballSpawnPoint;
    [SerializeField] protected float _hifForce = 10.0f;
    [SerializeField] protected GameObject _fooballPlayer;
    [SerializeField] protected ParticleSystem _particleSystem;
    [SerializeField] protected int _hitsRemained;

    protected Animator _animator;
    protected Vector3 _hitDirection = Vector3.forward;
    protected float _angleRotation = 5.0f;
    protected float _timeHitsReload = 3f;

    protected virtual void Start()
    {
        _animator = GetComponent<Animator>();
    }

    protected virtual void OnKickedAnimationFinished()
    {
        _particleSystem.Play();
        _ballRigidbody.AddForce(_hitDirection * _hifForce, ForceMode.Impulse);

        if (_hitsRemained > 0)
        {
            _hitsRemained--;
        }

        if (_hitsRemained <= 0)
            StartCoroutine(ReloadHits());

        Time.timeScale = 1f;
    }

    protected virtual IEnumerator ReloadHits()
    {
        var waitForSeconds = new WaitForSeconds(_timeHitsReload);

        if (_hitsRemained <= 0)
        {
            yield return waitForSeconds;
        }
    }

    protected virtual IEnumerator Kicking()
    {
        while (_hitsRemained > 0)
        {
            yield return null;
        }

        if (_hitsRemained <= 0)
        {
            _animator.SetBool(AnimatorPlayer.Params.IsAiming, false);
        }
    }
}

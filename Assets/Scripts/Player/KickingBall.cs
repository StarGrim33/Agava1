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
    [SerializeField] protected AnimatorPlayer _playerAnimator;
    [SerializeField] protected ParticleSystem _particleSystem;
    [SerializeField] protected int _hitsRemained;

    public event UnityAction OnBallKicked;

    protected Animator _animator;
    protected Vector3 _hitDirection = Vector3.forward;
    protected float _angleRotation = 5.0f;
    protected float _timeHitsReload = 3f;

    protected void OnEnable()
    {
        _playerAnimator.OnKickedAnimationFinished += OnKickedAnimationFinished;
    }

    protected void OnDisable()
    {
        _playerAnimator.OnKickedAnimationFinished -= OnKickedAnimationFinished;
    }

    protected void Start()
    {
        _animator = GetComponent<Animator>();
    }

    protected void OnKickedAnimationFinished()
    {
        OnBallKicked?.Invoke();
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

    private void Update()
    {
        if (_hitsRemained > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Time.timeScale = 0.3f;
                StartCoroutine(Kicking());
                _animator.SetBool(AnimatorPlayer.Params.IsAiming, true);
                _animator.Play(AnimatorPlayer.States.Strike, 0, 0f);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                Time.timeScale = 1f;
                _animator.SetBool(AnimatorPlayer.Params.IsAiming, false);
            }
        }
    }

    protected IEnumerator ReloadHits()
    {
        var waitForSeconds = new WaitForSeconds(_timeHitsReload);

        if (_hitsRemained <= 0)
        {
            yield return waitForSeconds;
            _hitsRemained = 3;
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

using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class KickingBall : MonoBehaviour
{
    private const string AxisName = "Mouse X";

    [SerializeField] private Rigidbody _ballRigidbody;
    [SerializeField] private Transform _ballSpawnPoint;
    [SerializeField] private float _hifForce = 10.0f;
    [SerializeField] private GameObject _player;
    [SerializeField] private AnimatorPlayer _playerAnimator;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private int _hitsRemained;

    public event UnityAction OnBallKicked;

    public bool IsAiming => _hitsRemained > 0;

    private Animator _animator;
    private Vector3 _hitDirection = Vector3.forward;
    private bool _isMouseDown = false;
    private float _angleRotation = 5.0f;
    private float _timeHitsReload = 3f;

    private void OnEnable()
    {
        _playerAnimator.OnKickedAnimationFinished += OnKickedAnimationFinished;
    }

    private void OnDisable()
    {
        _playerAnimator.OnKickedAnimationFinished -= OnKickedAnimationFinished;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnKickedAnimationFinished()
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
                _isMouseDown = true;
                Time.timeScale = 0.3f;
                StartCoroutine(Kicking());
                _animator.SetBool(AnimatorPlayer.Params.IsAiming, true);
                _animator.Play(AnimatorPlayer.States.Strike, 0, 0f);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                Time.timeScale = 1f;
                _isMouseDown = false;
                _animator.SetBool(AnimatorPlayer.Params.IsAiming, false);
            }
        }
    }

    private IEnumerator ReloadHits()
    {
        var waitForSeconds = new WaitForSeconds(_timeHitsReload);

        if (_hitsRemained <= 0)
        {
            yield return waitForSeconds;
            _hitsRemained = 3;
        }
    }

    private IEnumerator Kicking()
    {
        while (_isMouseDown && _hitsRemained > 0)
        {
            float mouseX = Input.GetAxis(AxisName);
            _player.transform.Rotate(0, mouseX * _angleRotation, 0);
            Quaternion rotation = Quaternion.Euler(0, mouseX * _angleRotation, 0);
            _hitDirection = rotation * _hitDirection;

            yield return null;
        }

        if (_hitsRemained <= 0)
        {
            _isMouseDown = false;
            _animator.SetBool(AnimatorPlayer.Params.IsAiming, false);
        }
    }
}

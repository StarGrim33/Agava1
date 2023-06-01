using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerKickingBall : KickingBall
{
    [SerializeField] private AnimatorPlayer _playerAnimator;
    [SerializeField] private int _hits;

    private const string AxisName = "Mouse X";

    public bool IsAiming => _hitsRemained > 0;

    public int HitsRemained => _hitsRemained;

    private bool _isMouseDown = false;
    private bool _canControlBall = true;

    private void OnEnable()
    {
        _playerAnimator.OnKickedAnimationFinished += OnKickedAnimationFinished;
    }

    private void OnDisable()
    {
        _playerAnimator.OnKickedAnimationFinished -= OnKickedAnimationFinished;
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

    public void SetBall(PlayerBall newBall)
    {
        var ball = newBall as PlayerBall;
        _ballRigidbody = ball.GetComponent<Rigidbody>();
    }

    protected override void OnKickedAnimationFinished()
    {
        _particleSystem.Play();
        _ballRigidbody.AddForce(_hitDirection * _hifForce, ForceMode.Impulse);
        _canControlBall = false;

        if (_hitsRemained > 0)
        {
            _hitsRemained--;
        }

        if (_hitsRemained <= 0)
            StartCoroutine(ReloadHits());

        Time.timeScale = 1f;
    }

    protected override IEnumerator Kicking()
    {
        _canControlBall = true;

        while (_isMouseDown && _hitsRemained > 0)
        {
            if(_canControlBall)
            {
                float mouseX = Input.GetAxis(AxisName);
                _fooballPlayer.transform.Rotate(0, mouseX * _angleRotation, 0);
                Quaternion rotation = Quaternion.Euler(0, mouseX * _angleRotation, 0);
                _hitDirection = rotation * _hitDirection;
            }

            yield return null;
        }

        if (_hitsRemained <= 0)
        {
            _isMouseDown = false;
            _animator.SetBool(AnimatorPlayer.Params.IsAiming, false);
        }
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
}

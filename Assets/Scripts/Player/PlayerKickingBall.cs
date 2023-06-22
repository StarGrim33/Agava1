using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerKickingBall : KickingBall
{
    [SerializeField] private AnimatorPlayer _playerAnimator;
    [SerializeField] private int _hits;
    [SerializeField] private GameObject _arrowImage;

    public bool IsAiming => _hitsRemained > 0;

    public int HitsRemained => _hitsRemained;

    private bool _isMouseDown = false;
    private bool _canControlBall = true;
    private float _currentHoldTime = 0f;
    private float _maxHoldTime = 1f;

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
            if (!_isMouseDown && EventSystem.current.currentSelectedGameObject == null && Input.GetMouseButtonDown(0))
            {
                _currentHoldTime = 0f;
                _isMouseDown = true;
                _arrowImage.SetActive(true);
                Time.timeScale = 0.2f;
                StartCoroutine(Kicking());
                _animator.SetBool(Constants.IsAiming, true);
                _animator.Play(AnimatorPlayer.States.Strike, 0, 0f);
            }
            else if (_isMouseDown && EventSystem.current.currentSelectedGameObject == null && Input.GetMouseButtonUp(0))
            {
                _isMouseDown = false;
                _arrowImage.SetActive(false);
                Time.timeScale = 1f;
                _animator.SetBool(Constants.IsAiming, false);
            }
            
        }

        if (_isMouseDown)
        {
            _currentHoldTime += Time.deltaTime;

            if (_currentHoldTime >= _maxHoldTime)
                PerformKick();
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
            _hitsRemained--;

        if (_hitsRemained <= 0)
            StartCoroutine(ReloadHits());

        Time.timeScale = 1f;
    }

    protected override IEnumerator Kicking()
    {
        _canControlBall = true;

        while (_isMouseDown && _hitsRemained > 0)
        {
            if (_canControlBall)
            {
                float mouseX = Input.GetAxis(Constants.AxisName);
                _fooballPlayer.transform.Rotate(0, mouseX * _angleRotation, 0);
                Quaternion rotation = Quaternion.Euler(0, mouseX * _angleRotation, 0);
                _hitDirection = rotation * _hitDirection;
            }

            yield return null;
        }

        if (_hitsRemained == 0)
        {
            _isMouseDown = false;
            _animator.SetBool(Constants.IsAiming, false);
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

    private void PerformKick()
    {
        if (_isMouseDown && _hitsRemained > 0)
        {
            _isMouseDown = false;
            _arrowImage.SetActive(false);
            Time.timeScale = 1f;
            _animator.SetBool(Constants.IsAiming, false);

            ReloadHits();
        }
    }
}

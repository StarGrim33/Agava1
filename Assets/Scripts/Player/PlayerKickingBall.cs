using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerKickingBall : KickingBall
{
    [SerializeField] private AnimatorPlayer _playerAnimator;
    [SerializeField] private int _hits;
    [SerializeField] private GameObject _arrowImage;
    private bool _isMouseDown = false;
    private bool _canControlBall = true;
    private float _currentHoldTime = 0f;
    private float _maxHoldTime = 1f;
    private float _timeDelayForAiming = 0.2f;

    public bool IsAiming => base.HitsRemained > 0;

    public int HitsRemained => base.HitsRemained;

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
        if (HitsRemained > 0)
        {
            if (!_isMouseDown && EventSystem.current.currentSelectedGameObject == null && Input.GetMouseButtonDown(0))
            {
                _currentHoldTime = 0f;
                _isMouseDown = true;
                _arrowImage.SetActive(true);
                Time.timeScale = _timeDelayForAiming;
                StartCoroutine(Kicking());
                Animator.SetBool(Constants.IsAiming, true);
                Animator.Play(AnimatorPlayer.States.Strike, 0, 0f);
            }
            else if (_isMouseDown && EventSystem.current.currentSelectedGameObject == null && Input.GetMouseButtonUp(0))
            {
                _isMouseDown = false;
                _arrowImage.SetActive(false);
                Time.timeScale = 1f;
                Animator.SetBool(Constants.IsAiming, false);
            }

        }

        if (_isMouseDown)
        {
            _currentHoldTime += Time.deltaTime;

            if (_currentHoldTime >= _maxHoldTime)
            {
                PerformKick();
            }
        }
    }

    public void SetBall(PlayerBall newBall)
    {
        var ball = newBall as PlayerBall;
        BallRigidbody = ball.GetComponent<Rigidbody>();
    }

    protected override void OnKickedAnimationFinished()
    {
        ParticleSystem.Play();
        BallRigidbody.AddForce(HitDirection * HifForce, ForceMode.Impulse);
        _canControlBall = false;

        if (base.HitsRemained > 0)
            base.HitsRemained--;

        if (HitsRemained <= 0)
            StartCoroutine(ReloadHits());

        Time.timeScale = 1f;
    }

    protected override IEnumerator Kicking()
    {
        _canControlBall = true;

        while (_isMouseDown && HitsRemained > 0)
        {
            if (_canControlBall)
            {
                float mouseX = Input.GetAxis(Constants.AxisName);
                FooballPlayer.transform.Rotate(0, mouseX * AngleRotation, 0);
                Quaternion rotation = Quaternion.Euler(0, mouseX * AngleRotation, 0);
                HitDirection = rotation * HitDirection;
            }

            yield return null;
        }

        if (HitsRemained == 0)
        {
            _isMouseDown = false;
            Animator.SetBool(Constants.IsAiming, false);
        }
    }

    protected override IEnumerator ReloadHits()
    {
        var waitForSeconds = new WaitForSeconds(TimeHitsReload);

        if (HitsRemained <= 0)
        {
            yield return waitForSeconds;
            base.HitsRemained = _hits;
        }
    }

    private void PerformKick()
    {
        if (_isMouseDown && HitsRemained > 0)
        {
            _isMouseDown = false;
            _arrowImage.SetActive(false);
            Time.timeScale = 1f;
            Animator.SetBool(Constants.IsAiming, false);
            StartCoroutine(ReloadHits());
        }
    }
}

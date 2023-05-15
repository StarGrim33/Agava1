using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerKickingBall : KickingBall
{
    private const string AxisName = "Mouse X";

    public bool IsAiming => _hitsRemained > 0;
    private bool _isMouseDown = false;

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

    protected override IEnumerator Kicking()
    {
        while (_isMouseDown && _hitsRemained > 0)
        {
            float mouseX = Input.GetAxis(AxisName);
            _fooballPlayer.transform.Rotate(0, mouseX * _angleRotation, 0);
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

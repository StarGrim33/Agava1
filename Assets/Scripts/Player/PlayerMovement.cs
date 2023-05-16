using System.Collections;
using UnityEngine;

public class PlayerMovement : BasePlayer
{
    [SerializeField] protected PlayerKickingBall _kickBall;

    protected override IEnumerator Teleport()
    {
        var waitForSeconds = new WaitForSeconds(_delay);

        if (Input.GetMouseButtonDown(0))
        {
            if (transform.position != _ball.transform.position && Vector3.Distance(transform.position, _ball.transform.position) > 1f && _kickBall.IsAiming)
            {
                transform.position = _ball.transform.position;
                _particleSystem.Play();
                _ball.StopMoving();
            }
        }

        yield return waitForSeconds;
    }
}

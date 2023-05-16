using System.Collections;
using UnityEngine;

public class EnemyMovement : BasePlayer
{
    [SerializeField] private EnemyKickingBall _kicking;

    protected override IEnumerator Teleport()
    {
        var waitForSeconds = new WaitForSeconds(_delay);

        if (transform.position != _ball.transform.position && Vector3.Distance(transform.position, _ball.transform.position) > 1f && _kicking.IsKicking)
        {
            transform.position = _ball.transform.position;
            _particleSystem.Play();
            _ball.StopMoving();
        }

        yield return waitForSeconds;
    }
}

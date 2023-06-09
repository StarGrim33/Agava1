using System.Collections;
using UnityEngine;

public class PlayerMovement : BasePlayer
{
    [SerializeField] protected PlayerKickingBall _kickBall;
    [SerializeField] private SphereCollider _sphereCollider;

    public void SetBall(PlayerBall newBall)
    {
        _ball = newBall;
    }

    protected override IEnumerator Teleport()
    {
        var waitForSeconds = new WaitForSeconds(_delay);
        float distanceToBall = 0.5f;

        if (Input.GetMouseButtonDown(0))
        {
            if (transform.position != _ball.transform.position && Vector3.Distance(transform.position, _ball.transform.position) > distanceToBall && _kickBall.IsAiming)
            {
                Vector3 direction = _ball.transform.position - transform.position;
                float distance = 1f;

                RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, distance);

                bool hasCollidersInPath = false;

                foreach (RaycastHit hit in hits)
                {
                    if (hit.collider != null && hit.collider != _sphereCollider)
                    {
                        hasCollidersInPath = true;
                        _ball.StopMoving();

                        break;
                    }
                }

                if (!hasCollidersInPath)
                {
                    transform.position = _ball.transform.position;
                    _particleSystem.Play();
                    _ball.StopMoving();
                }
            }
        }

        yield return waitForSeconds;
    }
}

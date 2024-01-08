using Ball;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;


public class PlayerMovement : BasePlayer
{
    [SerializeField] protected PlayerKickingBall KickBall;
    [SerializeField] private SphereCollider _sphereCollider;

    public void SetBall(PlayerBall newBall)
    {
        Ball = newBall;
    }

    protected override IEnumerator TeleportCoroutine()
    {
        var waitForSeconds = new WaitForSeconds(Delay);
        float distanceToBall = 0.5f;

        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.currentSelectedGameObject == null
                && transform.position != Ball.transform.position &&
                Vector3.Distance(transform.position, Ball.transform.position) > distanceToBall && KickBall.IsAiming)
            {
                Vector3 direction = Ball.transform.position - transform.position;
                float distance = 1f;

                RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, distance);

                bool hasCollidersInPath = false;

                foreach (RaycastHit hit in hits)
                {
                    if (hit.collider != null && hit.collider != _sphereCollider)
                    {
                        hasCollidersInPath = true;
                        Ball.StopMoving();

                        break;
                    }
                }

                if (!hasCollidersInPath)
                {
                    transform.position = Ball.transform.position;
                    ParticleSystem.Play();
                    Ball.StopMoving();
                }
            }
        }

        yield return waitForSeconds;
    }
}

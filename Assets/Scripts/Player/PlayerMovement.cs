using System.Collections;
using Ball;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Player
{
    public class PlayerMovement : PlayerBoundaryChecker
    {
        [SerializeField] protected PlayerKickingBall KickBall;
        [SerializeField] private SphereCollider _sphereCollider;
        private WaitForSeconds _waitForSeconds;
        private float _distanceToBall = 0.5f;

        protected override void Start()
        {
            _waitForSeconds = new WaitForSeconds(Delay);
            base.Start();
        }

        public void SetBall(PlayerBall newBall)
        {
            Ball = newBall;
        }

        protected override IEnumerator TeleportCoroutine()
        {
            if (Input.GetMouseButtonDown(0) && EventSystem.current.currentSelectedGameObject == null
                    && transform.position != Ball.transform.position &&
                    Vector3.Distance(transform.position, Ball.transform.position) > _distanceToBall && KickBall.IsAiming)
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

            yield return _waitForSeconds;
        }
    }
}

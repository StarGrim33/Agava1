using System.Collections;
using Ball;
using UnityEngine;

namespace Player
{
    public abstract class PlayerBoundaryChecker : MonoBehaviour
    {
        [SerializeField] protected BaseBall Ball;
        [SerializeField] protected ParticleSystem ParticleSystem;
        protected float Delay = 1f;
        protected bool IsTeleporting = false;
        private WaitForSeconds _waitForSeconds;

        protected virtual void Start()
        {
            _waitForSeconds = new WaitForSeconds(Delay);
        }

        protected void Update()
        {
            if (!IsTeleporting)
            {
                StartCoroutine(TeleportCoroutine());
            }
        }

        protected virtual IEnumerator TeleportCoroutine()
        {
            IsTeleporting = true;

            if (transform.position != Ball.transform.position)
            {
                Vector3 newPosition = Ball.transform.position;
                newPosition.y = 0f;
                transform.position = newPosition;
                ParticleSystem.Play();
                Ball.StopMoving();
            }

            yield return _waitForSeconds;

            IsTeleporting = false;
        }
    }
}

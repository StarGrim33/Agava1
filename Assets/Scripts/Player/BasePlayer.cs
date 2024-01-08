using Ball;
using System.Collections;
using UnityEngine;

public abstract class BasePlayer : MonoBehaviour
{
    [SerializeField] protected BaseBall Ball;
    [SerializeField] protected ParticleSystem ParticleSystem;
    protected float Delay = 1f;
    protected bool IsTeleporting = false;

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
        var waitForSeconds = new WaitForSeconds(Delay);

        if (transform.position != Ball.transform.position)
        {
            Vector3 newPosition = Ball.transform.position;
            newPosition.y = 0f;
            transform.position = newPosition;
            ParticleSystem.Play();
            Ball.StopMoving();
        }

        yield return waitForSeconds;

        IsTeleporting = false;
    }
}

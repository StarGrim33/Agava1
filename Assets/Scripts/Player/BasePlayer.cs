using System.Collections;
using UnityEngine;

public abstract class BasePlayer : MonoBehaviour
{
    [SerializeField] protected BaseBall Ball;
    [SerializeField] protected ParticleSystem ParticleSystem;
    [SerializeField] protected KickingBall KickingBall;
    protected float Delay = 1f;
    protected bool CanTeleport = false;

    protected void Update()
    {
        StartCoroutine(Teleport());
    }

    protected virtual IEnumerator Teleport()
    {
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
    }
}

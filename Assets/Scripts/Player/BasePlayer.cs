using System.Collections;
using UnityEngine;

public abstract class BasePlayer : MonoBehaviour
{
    [SerializeField] protected Ball _ball;
    [SerializeField] protected ParticleSystem _particleSystem;
    [SerializeField] protected KickingBall _kickingBall;

    protected float _delay = 1f;

    protected void Update()
    {
        StartCoroutine(Teleport());
    }

    protected virtual IEnumerator Teleport()
    {
        var waitForSeconds = new WaitForSeconds(_delay);

        if (transform.position != _ball.transform.position)
        {
            Vector3 newPosition = _ball.transform.position;
            newPosition.y = 0f;
            transform.position = newPosition;
            _particleSystem.Play();
            _ball.StopMoving();
        }

        yield return waitForSeconds;
    }
}

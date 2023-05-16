using System.Collections;
using UnityEngine;

public abstract class BasePlayer : MonoBehaviour
{
    [SerializeField] protected Ball _ball;
    [SerializeField] protected ParticleSystem _particleSystem;
    [SerializeField] protected KickingBall _kickingBall;

    protected float _delay = 1f;
    protected bool _canTeleport = false;

    protected void Update()
    {
        StartCoroutine(Teleport());
    }

    protected virtual IEnumerator Teleport()
    {
        var waitForSeconds = new WaitForSeconds(_delay);

        if (Input.GetMouseButtonDown(0))
        {
            if (transform.position != _ball.transform.position && Vector3.Distance(transform.position, _ball.transform.position) > 1f)
            {
                transform.position = _ball.transform.position;
                _particleSystem.Play();
                _ball.StopMoving();
            }
        }

        yield return waitForSeconds;
    }
}

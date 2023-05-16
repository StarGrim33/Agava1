using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] protected Ball _ball;
    [SerializeField] private GameObject _enemy;
    [SerializeField] protected ParticleSystem _particleSystem;
    [SerializeField] protected EnemyKickingBall _kickingBall;

    protected float _delay = 1f;

    protected void Update()
    {
        if (_kickingBall.IsKicking && Vector3.Distance(_enemy.transform.position, _ball.transform.position) > 1f)
           Teleport();
    }

    protected void Teleport()
    {
        _ball.StopMoving();
        Vector3 ballLastPosition = _ball.transform.position;
        ballLastPosition.y = 0f;
        Vector3 newPosition = transform.position;
        newPosition.y = 0f;

        _ball.transform.position = newPosition;
        _particleSystem.Play();
        transform.position = ballLastPosition;
    }
}

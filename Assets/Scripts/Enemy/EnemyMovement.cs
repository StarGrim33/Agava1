using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Ball _ball;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private EnemyKickingBall _kickingBall;

    private float _maxDistance = 1f;

    private void Update()
    {
        if (_kickingBall.IsKicking && Vector3.Distance(_enemy.transform.position, _ball.transform.position) > _maxDistance)
           Teleport();
    }

    private void Teleport()
    {
        _ball.StopMoving();
        Vector3 ballLastPosition = _ball.transform.position;
        ballLastPosition.y = 0f;
        Vector3 newPosition = transform.position;
        newPosition.y = 0f;

        _ball.transform.position = newPosition;
        _particleSystem.Play();
        transform.position = ballLastPosition;
        transform.LookAt(newPosition);
    }
}

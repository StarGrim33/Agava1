using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] protected Ball _ball;
    [SerializeField] private GameObject _enemy;
    //[SerializeField] protected ParticleSystem _particleSystem;
    [SerializeField] protected EnemyKickingBall _kickingBall;

    protected float _delay = 1f;

    //protected void Update()
    //{
        
    //    StartCoroutine(Teleport());
    //}

    protected virtual IEnumerator Teleport()
    {
        var waitForSeconds = new WaitForSeconds(_delay);

        if (Input.GetMouseButtonUp(0))
        {
            if (_enemy.transform.position != _ball.transform.position && _kickingBall.IsKicking && Vector3.Distance(_enemy.transform.position, _ball.transform.position) > 1f)
            {
                Vector3 newPosition = _ball.transform.position;
                newPosition.y = 0f;
                _enemy.transform.position = newPosition;
                //_particleSystem.Play();
                _ball.StopMoving();
            }
        }

        yield return waitForSeconds;
    }
}

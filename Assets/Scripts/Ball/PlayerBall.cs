using UnityEngine;

public class PlayerBall : Ball
{
    public override void StopMoving()
    {
        transform.position = _targetPosition.position;
        _rigidbody.velocity = Vector3.zero;
    }
}

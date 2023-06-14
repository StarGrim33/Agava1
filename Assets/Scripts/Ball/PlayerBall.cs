using UnityEngine;

public class PlayerBall : Ball
{
    [SerializeField] protected bool _isBuyed;

    public bool IsBuyed => _isBuyed;

    public override void StopMoving()
    {
        _rigidbody.velocity = Vector3.zero;
        transform.position = _targetPosition.position;
    }
}

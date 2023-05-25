using UnityEngine;

public class PlayerBall : Ball
{
    [SerializeField] protected bool _isBuyed;

    public bool IsBuyed => _isBuyed;

    public override void StopMoving()
    {
        transform.position = _targetPosition.position;
        _rigidbody.velocity = Vector3.zero;
    }
}

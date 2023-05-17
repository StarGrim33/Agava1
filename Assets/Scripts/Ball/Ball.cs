using UnityEngine;

public abstract class Ball : MonoBehaviour
{
    [SerializeField] protected Rigidbody _rigidbody;
    [SerializeField] protected Transform _targetPosition;

    public virtual void StopMoving()
    {
        _rigidbody.velocity = Vector3.zero;
    }
}

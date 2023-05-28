using UnityEngine;

public abstract class Ball : MonoBehaviour
{
    [SerializeField] protected Rigidbody _rigidbody;
    [SerializeField] protected Transform _targetPosition;
    protected float _maxXBorder = 25f;
    protected float _maxZBorder = 25f;

    protected virtual void Update()
    {
        CheckOutOfBounds();
    }

    public virtual void StopMoving()
    {
        _rigidbody.velocity = Vector3.zero;
    }

    protected void CheckOutOfBounds()
    {
        if (transform.position.x > _maxXBorder || transform.position.x < -_maxXBorder ||
            transform.position.z > _maxZBorder || transform.position.z < -_maxZBorder)
            HandleOutOfBounds();
    }

    protected virtual void HandleOutOfBounds()
    {
        Debug.Log("Worked");
        transform.position = Vector3.zero;
        StopMoving();
    }
}

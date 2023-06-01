using UnityEngine;

public abstract class Ball : MonoBehaviour
{
    [SerializeField] protected Rigidbody _rigidbody;
    [SerializeField] protected Transform _targetPosition;
    protected float _maxXBorder = 9.4f;
    protected float _minXBorder = -19.23f;
    protected float _maxZBorder = 16.81f;
    protected float _minZBorder = -4.83f;

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
        if (transform.position.x > _maxXBorder || transform.position.x < _minXBorder ||
            transform.position.z > _maxZBorder || transform.position.z < _minZBorder)
            HandleOutOfBounds();
    }

    protected virtual void HandleOutOfBounds()
    {
        Debug.Log("Worked");
        transform.position = _targetPosition.position;
        StopMoving();
    }

    protected void CheckColliders()
    {

    }
}

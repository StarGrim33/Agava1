using UnityEngine;

public abstract class Ball : MonoBehaviour
{
    [SerializeField] protected Rigidbody Rigidbody;
    [SerializeField] protected Transform TargetPosition;
    protected float MaxXBorder = 9.4f;
    protected float MinXBorder = -19.23f;
    protected float MaxZBorder = 16.81f;
    protected float MinZBorder = -4.83f;

    protected virtual void Update()
    {
        CheckOutOfBounds();
    }

    public virtual void StopMoving()
    {
        Rigidbody.velocity = Vector3.zero;
    }

    protected void CheckOutOfBounds()
    {
        if (transform.position.x > MaxXBorder || transform.position.x < MinXBorder ||
            transform.position.z > MaxZBorder || transform.position.z < MinZBorder)
            HandleOutOfBounds();
    }

    protected virtual void HandleOutOfBounds()
    {
        transform.position = TargetPosition.position;
        StopMoving();
    }
}

using UnityEngine;

public class PlayerBall : Ball
{
    [SerializeField] protected bool IsBuyåd;

    public bool IsBuyed => IsBuyåd;

    public override void StopMoving()
    {
        Rigidbody.velocity = Vector3.zero;
        transform.position = TargetPosition.position;
    }
}

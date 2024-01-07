using UnityEngine;

public class PlayerBall : BaseBall
{
    [SerializeField] protected bool IsBuyåd;

    public bool IsBuyed => IsBuyåd;

    public override void StopMoving()
    {
        Rigidbody.velocity = Vector3.zero;
        transform.position = TargetPosition.position;
    }
}

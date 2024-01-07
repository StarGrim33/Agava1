using UnityEngine;

public class PlayerBall : BaseBall
{
    [SerializeField] protected bool IsBuy�d;

    public bool IsBuyed => IsBuy�d;

    public override void StopMoving()
    {
        Rigidbody.velocity = Vector3.zero;
        transform.position = TargetPosition.position;
    }
}

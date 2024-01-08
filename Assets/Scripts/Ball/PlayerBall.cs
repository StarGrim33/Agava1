using UnityEngine;

public class PlayerBall : BaseBall
{
    [SerializeField] protected bool IsBuyåd;
    [SerializeField] protected PlayerData Data;

    public bool IsBallByed {  get; protected set; }

    protected virtual void Awake()
    {
        InitializeBall(Constants.BasketBall);
    }

    protected virtual void InitializeBall(string ballType)
    {
        if (PlayerPrefs.HasKey(ballType))
        {
            if (Data.IsBallPurchased(ballType))
            {
                IsBallByed = true;
                gameObject.SetActive(false);
            }
        }
        else
        {
            IsBallByed = false;
            gameObject.SetActive(false);
        }
    }

    public override void StopMoving()
    {
        Rigidbody.velocity = Vector3.zero;
        transform.position = TargetPosition.position;
    }
}

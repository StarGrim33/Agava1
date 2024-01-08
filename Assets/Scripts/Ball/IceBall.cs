public class IceBall : PlayerBall
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void InitializeBall(string ballType)
    {
        base.InitializeBall(Constants.IceBall);
    }
}

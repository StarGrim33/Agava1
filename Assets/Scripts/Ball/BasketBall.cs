namespace Ball
{
    public class BasketBall : PlayerBall
    {
        protected override void Awake()
        {
            base.Awake();
        }

        protected override void InitializeBall(string ballType)
        {
            base.InitializeBall(Constants.BasketBall);
        }
    }
}

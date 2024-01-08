namespace Ball
{
    public class SpiderBall : PlayerBall
    {
        protected override void Awake()
        {
            base.Awake();
        }

        protected override void InitializeBall(string ballType)
        {
            base.InitializeBall(Constants.SpiderBall);
        }
    }
}

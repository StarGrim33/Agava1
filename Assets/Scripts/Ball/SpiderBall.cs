using UnityEngine;

public class SpiderBall : PlayerBall
{
    [SerializeField] private PlayerData _data;

    private void Awake()
    {
        Initializing();
    }

    private void Initializing()
    {
        if (PlayerPrefs.HasKey(Constants.SpiderBall))
        {
            if (_data.IsBallPurchased(Constants.SpiderBall))
            {
                IsBuyåd = true;
                gameObject.SetActive(false);
            }

        }
        else
        {
            IsBuyåd = false;
            gameObject.SetActive(false);
        }
    }
}

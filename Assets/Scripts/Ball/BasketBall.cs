using UnityEngine;

public class BasketBall : PlayerBall
{
    [SerializeField] private PlayerData _data;

    private void Awake()
    {
        Initializing();
    }

    private void Initializing()
    {
        if (PlayerPrefs.HasKey(Constants.BasketBall))
        {
            if(_data.IsBallPurchased(Constants.BasketBall))
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

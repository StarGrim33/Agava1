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
                IsBuy�d = true;
                gameObject.SetActive(false);
            }

        }
        else
        {
            IsBuy�d = false;
            gameObject.SetActive(false);
        }
    }
}

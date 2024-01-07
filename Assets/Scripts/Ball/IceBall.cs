using UnityEngine;

public class IceBall : PlayerBall
{
    [SerializeField] private PlayerData _data;

    private void Awake()
    {
        Initializing();
    }

    private void Initializing()
    {
        if (PlayerPrefs.HasKey(Constants.IceBall))
        {
            if (_data.IsBallPurchased(Constants.IceBall))
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

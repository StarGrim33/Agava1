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

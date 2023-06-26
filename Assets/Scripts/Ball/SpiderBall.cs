using UnityEngine;

public class SpiderBall : PlayerBall
{
    [SerializeField] private PlayerData _data;

    private string _ballName = "_spiderBall";

    private void Awake()
    {
        Initializing();
    }

    private void Initializing()
    {
        if (PlayerPrefs.HasKey(_ballName))
        {
            if (_data.IsBallPurchased(_ballName))
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

using UnityEngine;

public class IceBall : PlayerBall
{
    [SerializeField] private PlayerData _data;

    private string _ballName = "_iceBall";

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

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
                _isBuyed = true;
                gameObject.SetActive(false);
                Debug.Log("Успешно");
            }

        }
        else
        {
            _isBuyed = false;
            gameObject.SetActive(false);
            Debug.Log("Не успешно");
        }
    }
}

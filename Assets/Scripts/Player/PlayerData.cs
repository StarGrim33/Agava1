using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public const string BasketBall = "_basketBall";
    public const string IceBall = "_iceBall";
    public const string SpiderBall = "_spiderBall";

    public bool IsBallPurchased(string ballName)
    {
        string key = GetBallPurchaseKey(ballName);
        return PlayerPrefs.GetInt(key, 0) == 1;
    }

    public void SetBallPurchased(string ballName)
    {
        string key = GetBallPurchaseKey(ballName);
        PlayerPrefs.SetInt(key, 1);
        PlayerPrefs.Save();
    }

    private string GetBallPurchaseKey(string ballName)
    {
        string key = string.Empty;

        switch (ballName)
        {
            case BasketBall:
                key = BasketBall;
                break;
            case IceBall:
                key = IceBall;
                break;
            case SpiderBall:
                key = SpiderBall;
                break;
        }

        return key;
    }
}

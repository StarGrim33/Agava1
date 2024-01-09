using UnityEngine;
using Utils;

namespace Player
{
    public class PlayerData : MonoBehaviour
    {
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

            if (ballName == Constants.BasketBall)
            {
                key = Constants.BasketBall;
            }
            else if (ballName == Constants.IceBall)
            {
                key = Constants.IceBall;
            }
            else if (ballName == Constants.SpiderBall)
            {
                key = Constants.SpiderBall;
            }

            return key;
        }
    }
}

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

            switch (ballName)
            {
                case Constants.BasketBall:
                    key = Constants.BasketBall;
                    break;

                case Constants.SpiderBall:
                    key = Constants.SpiderBall;
                    break;

                case Constants.IceBall:
                    key = Constants.IceBall;
                    break;
            }

            return key;
        }
    }
}

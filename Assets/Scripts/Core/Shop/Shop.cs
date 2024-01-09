using System.Collections.Generic;
using Agava.YandexGames;
using Player;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Core
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private List<ShopItemsSO> _items;
        [SerializeField] private List<GameObject> _shopPanelsSO;
        [SerializeField] private List<ShopTemplate> _templates;
        [SerializeField] private Button[] _purchaseButtons;
        [SerializeField] private PlayerTotalScore _totalScore;
        [SerializeField] private ScoreDisplayer _scoreDisplayer;
        [SerializeField] private PlayerData _playerData;

        private Dictionary<int, string> _ballNames = new()
    {
        { (int)Balls.BasketBall, Constants.BasketBall },
        { (int)Balls.IceBall, Constants.IceBall },
        { (int)Balls.SpiderBall, Constants.SpiderBall },
    };

        private void Start()
        {
            LoadPanels();
            CheckPurchaseable();
        }

        public void PurchaseItem(int buttonIndex)
        {
            int score = PlayerPrefs.GetInt(Constants.TotalScoreKey);

            if (score >= _items[buttonIndex].BaseCost)
            {
                if (!_playerData.IsBallPurchased(_ballNames[buttonIndex]))
                {
                    _playerData.SetBallPurchased(_ballNames[buttonIndex]);
                    _totalScore.ReduceScore(_items[buttonIndex].BaseCost);
                    _scoreDisplayer.UpdateCoinCountText();
                    _purchaseButtons[buttonIndex].interactable = false;
                    InterstitialAd.Show();
                    PlayerPrefs.Save();
                }

                return;
            }
        }

        private void LoadPanels()
        {
            foreach (GameObject panel in _shopPanelsSO)
            {
                panel.SetActive(true);
            }

            for (int i = 0; i < _items.Count; i++)
            {
                _templates[i].Title.text = _items[i].Title;
                _templates[i].Desription.text = _items[i].Desription;
                _templates[i].BaseCost.text = _items[i].BaseCost.ToString();
                _templates[i].Image.sprite = _items[i].Sprite;
                _templates[i].SetBallId(_items[i].BallId);
            }
        }

        private void CheckPurchaseable()
        {
            int score = PlayerPrefs.GetInt(Constants.TotalScoreKey);

            for (int i = 0; i < _items.Count; i++)
            {
                if (score >= _items[i].BaseCost)
                {
                    _purchaseButtons[i].interactable = !_playerData.IsBallPurchased(_ballNames[i]);
                }
                else
                {
                    _purchaseButtons[i].interactable = false;
                }
            }
        }
    }
}

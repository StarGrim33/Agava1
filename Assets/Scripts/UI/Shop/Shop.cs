using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<ShopItemsSO> _items;
    [SerializeField] private List<GameObject> _shopPanelsSO;
    [SerializeField] private List<ShopTemplate> _templates;
    [SerializeField] private Button[] _purchaseButtons;
    [SerializeField] private PlayerTotalScore _totalScore;
    [SerializeField] private ScoreDisplayer _scoreDisplayer;
    [SerializeField] private PlayerData _playerData;

    private void Start()
    {
        LoadPanels();
        CheckPurchaseable();
    }

    private void LoadPanels()
    {
        for (int i = 0; i < _shopPanelsSO.Count; i++)
        {
            _shopPanelsSO[i].SetActive(true);
        }

        for (int i = 0; i < _items.Count; i++)
        {
            _templates[i].Title.text = _items[i].Title;
            _templates[i].Desription.text = _items[i].Desription;
            _templates[i].BaseCost.text = "Цена:" + _items[i].BaseCost.ToString();
            _templates[i].Image.sprite = _items[i].Sprite;
            _templates[i].BallId = _items[i].BallId;
        }
    }

    private void CheckPurchaseable()
    {
        int score = PlayerPrefs.GetInt(PlayerTotalScore.TotalScoreKey);

        for (int i = 0; i < _items.Count; i++)
        {
            if (score >= _items[i].BaseCost)
            {
                if (!_playerData.IsBallPurchased("_basketBall"))
                    _purchaseButtons[i].interactable = true;
                else if (!_playerData.IsBallPurchased("_iceBall"))
                    _purchaseButtons[i].interactable = true;
                else if (!_playerData.IsBallPurchased("_spiderBall"))
                    _purchaseButtons[i].interactable = true;
                else
                    _purchaseButtons[i].interactable = false;
            }
            else
                _purchaseButtons[i].interactable = false;
        }
    }

    public void PurchaseItem(int buttonIndex)
    {
        int score = PlayerPrefs.GetInt(PlayerTotalScore.TotalScoreKey);

        if (score >= _items[buttonIndex].BaseCost)
        {
            if (buttonIndex == (int)Balls.BasketBall && _playerData.IsBallPurchased(PlayerData.BasketBall) == false)
            {
                _playerData.SetBallPurchased("_basketBall");
                _totalScore.ReduceScore(_items[(int)Balls.BasketBall].BaseCost);
                _scoreDisplayer.UpdateCoinCountText();
                _purchaseButtons[(int)Balls.BasketBall].interactable = false;

                PlayerPrefs.Save();
            }
            else if (buttonIndex == (int)Balls.IceBall && _playerData.IsBallPurchased(PlayerData.IceBall) == false)
            {
                _playerData.SetBallPurchased("_iceBall");
                _totalScore.ReduceScore(_items[(int)Balls.IceBall].BaseCost);
                _scoreDisplayer.UpdateCoinCountText();
                _purchaseButtons[(int)Balls.IceBall].interactable = false;

                PlayerPrefs.Save();
            }
            else if (buttonIndex == (int)Balls.SpiderBall && _playerData.IsBallPurchased(PlayerData.SpiderBall) == false)
            {
                _playerData.SetBallPurchased("_spiderBall");
                _totalScore.ReduceScore(_items[(int)Balls.SpiderBall].BaseCost);
                _scoreDisplayer.UpdateCoinCountText();
                _purchaseButtons[(int)Balls.SpiderBall].interactable = false;

                PlayerPrefs.Save();
            }

            return;
        }
    }

    private enum Balls
    {
        BasketBall,
        IceBall,
        SpiderBall
    }
}

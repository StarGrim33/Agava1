using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Localization;
using Agava.YandexGames;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<ShopItemsSO> _items;
    [SerializeField] private List<GameObject> _shopPanelsSO;
    [SerializeField] private List<ShopTemplate> _templates;
    [SerializeField] private Button[] _purchaseButtons;
    [SerializeField] private PlayerTotalScore _totalScore;
    [SerializeField] private ScoreDisplayer _scoreDisplayer;
    [SerializeField] private PlayerData _playerData;

    private readonly Dictionary<int, string> _ballNames = new()
    {
        {(int)Balls.BasketBall, "_basketBall"},
        {(int)Balls.IceBall, "_iceBall"},
        {(int)Balls.SpiderBall, "_spiderBall"}
    };

    private void Start()
    {
        LoadPanels();
        CheckPurchaseable();
    }

    public void PurchaseItem(int buttonIndex)
    {
        int score = PlayerPrefs.GetInt(PlayerTotalScore.TotalScoreKey);

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
                if (!_playerData.IsBallPurchased(_ballNames[i]))
                    _purchaseButtons[i].interactable = true;
                else
                    _purchaseButtons[i].interactable = false;
            }
            else
                _purchaseButtons[i].interactable = false;
        }
    }

    private enum Balls
    {
        BasketBall,
        IceBall,
        SpiderBall
    }
}

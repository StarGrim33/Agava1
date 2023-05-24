using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    private const string _itemsKey = "_itemsUnlocked";

    [SerializeField] private List<ShopItemsSO> _items;
    [SerializeField] private List<GameObject> _shopPanelsSO;
    [SerializeField] private List<ShopTemplate> _templates;
    [SerializeField] private Button[] _purchaseButtons;
    [SerializeField] private PlayerTotalScore _totalScore;
    [SerializeField] private ScoreDisplayer _scoreDisplayer;

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
        }
    }

    private void CheckPurchaseable()
    {
        int score = PlayerPrefs.GetInt(PlayerTotalScore.TotalScoreKey);

        for (int i = 0; i < _items.Count; i++)
        {
            if (score >= _items[i].BaseCost)
                _purchaseButtons[i].interactable = true;
            else
                _purchaseButtons[i].interactable = false;
        }
    }

    public void PurchaseItem(int buttonIndex)
    {
        int score = PlayerPrefs.GetInt(PlayerTotalScore.TotalScoreKey);

        if (score >= _items[buttonIndex].BaseCost && _items[buttonIndex].IsBuyed != true)
        {
            _items[buttonIndex].SetBuyStatus();
            _totalScore.ReduceScore(_items[buttonIndex].BaseCost);
            _scoreDisplayer.UpdateCoinCountText();
            _purchaseButtons[buttonIndex].interactable = false;
        }
    }
}

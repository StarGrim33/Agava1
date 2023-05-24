using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ShopMenu", menuName = "Scriptable Objects/New Shop Item", order = 1)]
public class ShopItemsSO : ScriptableObject
{
    [SerializeField] private Sprite _image;
    [SerializeField] private string _title;
    [SerializeField] private string _desription;
    [SerializeField] private int _baseCost;

    public Sprite Sprite => _image;

    public string Title => _title;

    public string Desription => _desription;

    public int BaseCost => _baseCost;
}

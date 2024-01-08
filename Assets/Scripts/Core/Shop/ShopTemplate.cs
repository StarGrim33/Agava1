using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopTemplate : MonoBehaviour
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _desription;
    [SerializeField] private TMP_Text _baseCost;
    [SerializeField] private Image _image;
    [SerializeField] private int _ballId;

    public Image Image { get; private set; }

    public TMP_Text Title => _title;

    public TMP_Text Desription => _desription;

    public TMP_Text BaseCost => _baseCost;

    public void SetBallId(int value)
    {
        if (value > 0)
        {
            _ballId = value;
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Scrollbar))]
public class ScrollableMenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject _scrollbar;
    private Scrollbar _scrollbarComponent;
    private float _scrollPosition = 0;
    private float[] _scrollPositions;
    private float _xPosition = 1.5f;
    private float _yPosition = 1.5f;
    private float _lerpTime = 0.1f;
    private float _innerXPosition = 0.8f;
    private float _innerYPosition = 0.8f;
    private int _divider = 2;

    private void Start()
    {
        _scrollbarComponent = _scrollbar.GetComponent<Scrollbar>();
    }

    private void Update()
    {
        _scrollPositions = new float[transform.childCount];
        float distance = 1f / (_scrollPositions.Length - 1f);

        for (int i = 0; i < _scrollPositions.Length; i++)
            _scrollPositions[i] = distance * i;

        if (Input.GetMouseButton(0))
        {
            _scrollPosition = _scrollbarComponent.value;
        }
        else
        {
            for (int i = 0; i < _scrollPositions.Length; i++)
            {
                if (_scrollPosition < _scrollPositions[i] + (distance / _divider)
                    && _scrollPosition > _scrollPositions[i] - (distance / _divider))
                    _scrollbarComponent.value = Mathf.Lerp(_scrollbarComponent.value, _scrollPositions[i], _lerpTime);
            }
        }

        for (int i = 0; i < _scrollPositions.Length; i++)
        {
            if (_scrollPosition < _scrollPositions[i] + (distance / _divider) 
                && _scrollPosition > _scrollPositions[i] - (distance / _divider))
            {
                transform.GetChild(i).localScale = 
                    Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(_xPosition, _yPosition), _lerpTime);

                for(int a = 0;  a < _scrollPositions.Length; a++)
                {
                    transform.GetChild(a).localScale = 
                        Vector2.Lerp(transform.GetChild(a).localScale, new Vector2(_innerXPosition, _innerYPosition), _lerpTime);
                }
            }
        }
    }
}

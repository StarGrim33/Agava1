using UnityEngine;
using UnityEngine.UI;

public class SwiperMenu : MonoBehaviour
{
    [SerializeField] private GameObject _scrollbar;
    private float _scrollPosition = 0;
    private float[] _scrollPositions;

    private void Update()
    {
        _scrollPositions = new float[transform.childCount];
        float distance = 1f / (_scrollPositions.Length - 1f);

        for (int i = 0; i < _scrollPositions.Length; i++)
            _scrollPositions[i] = distance * i;

        if (Input.GetMouseButton(0))
        {
            _scrollPosition = _scrollbar.GetComponent<Scrollbar>().value;
        }
        else
        {
            for (int i = 0; i < _scrollPositions.Length; i++)
            {
                if (_scrollPosition < _scrollPositions[i] + (distance / 2) && _scrollPosition > _scrollPositions[i] - (distance / 2))
                    _scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(_scrollbar.GetComponent<Scrollbar>().value, _scrollPositions[i], 0.1f);
            }
        }


        for (int i = 0; i < _scrollPositions.Length; i++)
        {
            if (_scrollPosition < _scrollPositions[i] + (distance / 2) && _scrollPosition > _scrollPositions[i] - (distance / 2))
            {
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1.5f, 1.5f), 0.1f);

                for(int a = 0;  a < _scrollPositions.Length; a++)
                {
                    transform.GetChild(a).localScale = Vector2.Lerp(transform.GetChild(a).localScale, new Vector2(0.8f, 0.8f), 0.1f);
                }
            }
        }
    }
}

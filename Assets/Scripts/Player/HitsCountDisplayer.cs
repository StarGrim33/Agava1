using System.Collections;
using TMPro;
using UnityEngine;

public class HitsCountDisplayer : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private PlayerKickingBall _kickingBall;

    private void Update()
    {
        StartCoroutine(HitsDisplaying());
    }

    private IEnumerator HitsDisplaying()
    {
        _text.text = _kickingBall.HitsRemained.ToString();
        yield return null;
    }
}

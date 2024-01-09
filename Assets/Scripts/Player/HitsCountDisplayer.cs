using TMPro;
using UnityEngine;

namespace Player
{
    public class HitsCountDisplayer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private PlayerKickingBall _kickingBall;

        private void OnEnable()
        {
            _kickingBall.OnHitsRemainedChanged += UpdateHitsDisplaying;
        }

        private void OnDisable()
        {
            _kickingBall.OnHitsRemainedChanged -= UpdateHitsDisplaying;
        }

        private void UpdateHitsDisplaying(int hitsRemained)
        {
            _text.text = hitsRemained.ToString();
        }
    }
}

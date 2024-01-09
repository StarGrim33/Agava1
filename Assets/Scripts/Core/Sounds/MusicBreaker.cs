using Agava.WebUtility;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class MusicBreaker : MonoBehaviour, IMusicHandler
    {
        [SerializeField] private Button _button;
        [SerializeField] private Sprite _turnedOffMusicImage;
        [SerializeField] private Sprite _turnedOnMusicImage;
        [SerializeField] private AudioYB _audioYB;
        [SerializeField] private Image _startImage;

        private void OnEnable()
        {
            WebApplication.InBackgroundChangeEvent += OnInBackgroundChange;
        }

        private void OnDisable()
        {
            WebApplication.InBackgroundChangeEvent -= OnInBackgroundChange;
        }

        private void Start()
        {
            if (_audioYB.isPlaying)
            {
                _startImage.sprite = _turnedOnMusicImage;
            }
        }

        public void TurnMusic()
        {
            if (_audioYB.isPlaying)
            {
                _audioYB.Pause();
                _startImage.sprite = _turnedOffMusicImage;
            }
            else
            {
                _audioYB.UnPause();
                _startImage.sprite = _turnedOnMusicImage;
            }
        }

        private void OnInBackgroundChange(bool inBackground)
        {
            AudioListener.pause = inBackground;
            AudioListener.volume = inBackground ? 0f : 1f;
        }
    }
}


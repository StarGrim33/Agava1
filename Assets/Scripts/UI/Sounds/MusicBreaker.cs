using Agava.WebUtility;
using UnityEngine;
using UnityEngine.UI;

public class MusicBreaker : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Sprite _turnedOffMusicImage;
    [SerializeField] private Sprite _turnedOnMusicImage;
    [SerializeField] private AudioYB _audioYB;

    private Image _startImage;

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
        _startImage = GetComponent<Image>();

        if (_audioYB.isPlaying)
        {
            _startImage.sprite = _turnedOnMusicImage;
        }
    }

    public void TurnMusic()
    {
        if(_audioYB.isPlaying)
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


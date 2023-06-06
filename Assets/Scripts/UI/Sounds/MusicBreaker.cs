using Agava.WebUtility;
using UnityEngine;
using UnityEngine.UI;

public class MusicBreaker : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Sprite _turnedOffMusicImage;
    [SerializeField] private Sprite _turnedOnMusicImage;
    [SerializeField] private AudioSource _audioSource;

    private Image _startImage;
    private bool _isPlaying;

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

        if(_audioSource != null && _audioSource.isPlaying)
        {
            _startImage.sprite = _turnedOnMusicImage;
            _isPlaying = true;
        }
    }

    public void TurnOffOrOnMusic()
    {
        if(_isPlaying)
        {
            _audioSource.Pause();
            _startImage.sprite = _turnedOffMusicImage;
            _isPlaying = false;
        }
        else
        {
            _audioSource.Play();
            _startImage.sprite = _turnedOnMusicImage;
            _isPlaying = true;
        }
    }

    private void OnInBackgroundChange(bool inBackground)
    {
        AudioListener.pause = inBackground;
        AudioListener.volume = inBackground ? 0f : 1f;
    }
}


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
            _audioSource.Stop();
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
}


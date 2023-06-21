using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    [SerializeField] private AudioYB _audio;
    [SerializeField] private string _audioClip;

    private void Start()
    {
        _audio.Play(_audioClip);
    }
}

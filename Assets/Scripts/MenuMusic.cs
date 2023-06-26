using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMusic : MonoBehaviour
{
    [SerializeField] private AudioYB _audio;
    [SerializeField] private string _audioClip;

    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if(Time.timeScale == 0 &&  currentScene.buildIndex != 2 && currentScene.buildIndex != 1)
            Time.timeScale = 1;

        if (_audio.isPlaying == false)
            _audio.Play(_audioClip);
    }
}

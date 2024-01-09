using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class MenuMusic : MonoBehaviour
    {
        [SerializeField] private AudioYB _audio;
        [SerializeField] private string _audioClip;

        private void Start()
        {
            int firstLevelIndex = 2;
            int menuSceneIndex = 1;
            Scene currentScene = SceneManager.GetActiveScene();

            if (Time.timeScale == 0 && currentScene.buildIndex != firstLevelIndex && currentScene.buildIndex != menuSceneIndex)
                Time.timeScale = 1;

            if (_audio.isPlaying == false)
                _audio.Play(_audioClip);
        }
    }
}

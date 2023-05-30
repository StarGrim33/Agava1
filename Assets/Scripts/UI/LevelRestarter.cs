using UnityEngine;
using Agava.YandexGames;
using UnityEngine.SceneManagement;

public class LevelRestarter : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    public void Restart()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        Time.timeScale = 1.0f;
    }

    public void RestartWithAD()
    {
        _audioSource.Stop();
        VideoAd.Show();
        Restart();
    }
}

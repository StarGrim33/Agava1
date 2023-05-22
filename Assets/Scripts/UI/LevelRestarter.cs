using UnityEngine;
using Agava.YandexGames;
using UnityEngine.SceneManagement;

public class LevelRestarter : MonoBehaviour
{
    public void Restart()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        Time.timeScale = 1.0f;
    }

    public void RestartWithAD()
    {
        VideoAd.Show();
        Restart();
    }
}

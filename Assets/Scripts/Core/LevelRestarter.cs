using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelRestarter : MonoBehaviour, ILevelRestart
{
    public void Restart()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}

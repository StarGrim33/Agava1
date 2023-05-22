using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitcher : MonoBehaviour
{
    private const string LevelsUnlocked = "_levelsUnlocked";

    public void UnlockNextLevel()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;

        currentLevelIndex += 1;
        PlayerPrefs.SetInt(LevelsUnlocked, currentLevelIndex);
        SceneManager.LoadScene(1);
    }
}

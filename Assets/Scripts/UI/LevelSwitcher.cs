using UnityEngine;
using Agava.YandexGames;
using UnityEngine.SceneManagement;

public class LevelSwitcher : MonoBehaviour
{
    private const string LevelsUnlocked = "_levelsUnlocked";

    [SerializeField] private Score _score;

    public void UnlockNextLevel()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        int nextLevelIndex = currentLevelIndex;

        int maxLevelIndex = SceneManager.sceneCountInBuildSettings - 1; 

        if (nextLevelIndex <= maxLevelIndex)
        {
            PlayerPrefs.SetInt(LevelsUnlocked, nextLevelIndex);
            PlayerPrefs.Save();
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(1);
            Debug.Log("Последний уровень достигнут");
        }
    }

    public void UnlockNextLevelWithAD()
    {
        VideoAd.Show();
        _score.DoubleScoreForAD();
        UnlockNextLevel();
    }
}

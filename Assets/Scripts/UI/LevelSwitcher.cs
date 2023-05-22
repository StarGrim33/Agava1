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

        currentLevelIndex += 1;
        PlayerPrefs.SetInt(LevelsUnlocked, currentLevelIndex);
        SceneManager.LoadScene(1);
    }

    public void UnlockNextLevelWithAD()
    {
        VideoAd.Show();
        _score.DoubleScoreForAD();
    }
}

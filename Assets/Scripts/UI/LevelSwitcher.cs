using Agava.YandexGames;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitcher : MonoBehaviour, IAdShow
{
    private const string LevelsUnlocked = "_levelsUnlocked";

    [SerializeField] private Score _score;
    [SerializeField] private AudioYB _audioSource;

    public void UnlockNextLevel()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        int nextLevelIndex = currentLevelIndex;

        int maxLevelIndex = SceneManager.sceneCountInBuildSettings - 1;

        if (nextLevelIndex <= maxLevelIndex)
        {
            PlayerPrefs.SetInt(LevelsUnlocked, nextLevelIndex);
            PlayerPrefs.Save();
            VideoAd.Show(OnOpenCallback, null, OnCloseCallback, OnErrorCallback);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }

    public void UnlockNextLevelWithAD()
    {
        _score.DoubleScoreForAD();
        UnlockNextLevel();
    }

    private void OnCloseCallback()
    {
        ContinueGame();
    }

    private void ContinueGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);

        if (_audioSource.clip != null)
            _audioSource.Play();
    }

    private void PauseGame()
    {
        Time.timeScale = 0;

        if (_audioSource.clip != null)
            _audioSource.Pause();
    }

    private void OnOpenCallback()
    {
        PauseGame();
    }

    private void OnErrorCallback(string errorMessage)
    {
        ContinueGame();
    }
}

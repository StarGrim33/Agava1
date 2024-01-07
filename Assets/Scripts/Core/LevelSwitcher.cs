using Agava.YandexGames;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitcher : MonoBehaviour, IAdShow, IGameStatesHandler
{
    [SerializeField] private Score _score;
    [SerializeField] private AudioYB _audioSource;

    public void UnlockNextLevel()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        int nextLevelIndex = currentLevelIndex;

        int maxLevelIndex = SceneManager.sceneCountInBuildSettings - 1;

        if (nextLevelIndex <= maxLevelIndex)
        {
            PlayerPrefs.SetInt(Constants.LevelsUnlocked, nextLevelIndex);
            PlayerPrefs.Save();
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }

    public void UnlockNextLevelWithAD()
    {
        _score.DoubleScoreForAD();
        VideoAd.Show(OnOpenCallback, null, OnCloseCallback, OnErrorCallback);
        UnlockNextLevel();
    }

    public void ContinueGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);

        if (_audioSource.clip != null)
            _audioSource.Play();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;

        if (_audioSource.clip != null)
            _audioSource.Pause();
    }

    private void OnCloseCallback() => ContinueGame();

    private void OnOpenCallback() => PauseGame();

    private void OnErrorCallback(string errorMessage) => ContinueGame();
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneOpener : MonoBehaviour
{
    [SerializeField] private int _levelNumber;

    public void OpenScene()
    {
        PlayerPrefs.Save();
        SceneManager.LoadScene(_levelNumber);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneOpener : MonoBehaviour
{
    [SerializeField] private int _levelNumber;

    public void OpenScene()
    {
        SceneManager.LoadScene(_levelNumber);
    }
}

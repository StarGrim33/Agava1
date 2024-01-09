using Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class SceneOpener : MonoBehaviour, ISceneOpener
    {
        [SerializeField] private int _levelNumber;

        public void OpenScene()
        {
            PlayerPrefs.Save();
            SceneManager.LoadScene(_levelNumber);
        }

        public void OpenRandomScene()
        {
            PlayerPrefs.Save();
            int randomIndex = ChooseRandomScene();
            SceneManager.LoadScene(randomIndex);
        }

        private int ChooseRandomScene()
        {
            int firstLevel = 0;
            int lastLevel = 12;
            int randomIndex = Random.Range(firstLevel, lastLevel + 1);
            return randomIndex;
        }
    }
}

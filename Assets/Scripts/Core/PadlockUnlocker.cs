using UnityEngine;
using Utils;

namespace Core
{
    public class PadlockUnlocker : MonoBehaviour
    {
        [SerializeField] private GameObject _padlock;
        [SerializeField] private int _level;

        private void Start()
        {
            int defaultValue = 1;
            int levelUnlocked = PlayerPrefs.GetInt(Constants.LevelsUnlocked, defaultValue);

            if (levelUnlocked >= _level)
                _padlock.SetActive(false);
        }
    }
}

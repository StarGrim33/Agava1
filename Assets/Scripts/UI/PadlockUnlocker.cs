using UnityEngine;

public class PadlockUnlocker : MonoBehaviour
{
    [SerializeField] private GameObject _padlock;
    [SerializeField] private int _level;

    private void Start()
    {
        string levelsUnlocked = "_levelsUnlocked";

        int levelUnlocked = PlayerPrefs.GetInt(levelsUnlocked, _level);

        if(levelUnlocked >= _level)
        {
            int previousLevel = _level - 1;
            bool previousLevelPassed = PlayerPrefs.GetInt(levelsUnlocked, 0) >= previousLevel;

            if (previousLevelPassed)
                _padlock.SetActive(false);
        }
    }
}

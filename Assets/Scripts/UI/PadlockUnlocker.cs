using UnityEngine;

public class PadlockUnlocker : MonoBehaviour
{
    [SerializeField] private GameObject _padlock;
    [SerializeField] private int _level;

    private void Start()
    {
        string levelsUnlocked = "_levelsUnlocked";

        int levelUnlocked = PlayerPrefs.GetInt(levelsUnlocked, 1);

        if (levelUnlocked >= _level)
        {
            _padlock.SetActive(false);
        }
    }
}

using UnityEngine;

public class PadlockUnlocker : MonoBehaviour
{
    [SerializeField] private GameObject _padlock;
    [SerializeField] private int _level;

    private void Start()
    {
        int levelUnlocked = PlayerPrefs.GetInt(Constants.LevelsUnlocked, 1);

        if (levelUnlocked >= _level)
            _padlock.SetActive(false);
    }
}

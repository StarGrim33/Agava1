using UnityEngine;
using UnityEngine.UI;
using Utils;

public class LevelUnlocker : MonoBehaviour
{
    [SerializeField] private Button[] _buttons;

    private int _levelsUnlocked;
    
    private void Start()
    {
        _levelsUnlocked = PlayerPrefs.GetInt(Constants.LevelsUnlocked, 1);

        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].interactable = i < _levelsUnlocked;
        }

        PlayerPrefs.Save();
    }
}

using Agava.YandexGames;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelUnlocker : MonoBehaviour
{
    [SerializeField] private Button[] _buttons;

    private const string LevelsUnlocked = "_levelsUnlocked";

    private int _levelsUnlocked;
    
    private void Start()
    {
        _levelsUnlocked = PlayerPrefs.GetInt(LevelsUnlocked, 1);

        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].interactable = (i < _levelsUnlocked);
        }

        PlayerPrefs.Save();
    }
}

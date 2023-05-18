using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelUnlocker : MonoBehaviour
{
    private const string LevelsUnlocked = "_levelsUnlocked";

    [SerializeField] private Button[] _buttons;
    private int _levelsUnlocked;
    private int _currentLevel;

    private void Start()
    {
        _levelsUnlocked = PlayerPrefs.GetInt(LevelsUnlocked, 1);

        for(int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].interactable = false;
        }

        for (int i = 0; i < _levelsUnlocked; i++)
        {
            _buttons[i].interactable = true;
        }
    }

    private void UnlockNextLevel()
    {
        if(_currentLevel > PlayerPrefs.GetInt(LevelsUnlocked))
        {
            _currentLevel +=1;
            PlayerPrefs.SetInt(LevelsUnlocked, _currentLevel);
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
            _buttons[i].interactable = false;
        }

        for (int i = 0; i < _levelsUnlocked - 1; i++)
        {
            _buttons[i].interactable = true;
        }
    }
}

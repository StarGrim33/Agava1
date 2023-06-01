using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialShower : MonoBehaviour
{
    private const string TurorialShowed = "_tutorialShowed";

    [SerializeField] private List<Button> _tutorials;
    [SerializeField] private GameObject _player;

    private bool _showTutorial;
    private int _currentTutorialIndex = 0;

    private void Awake()
    {
        _showTutorial = PlayerPrefs.GetInt(TurorialShowed) == 0;

        if (_showTutorial)
            ShowTutorial();
    }

    public void OnTutorialClick()
    {
        if(_currentTutorialIndex + 1 < _tutorials.Count)
        {
            _tutorials[_currentTutorialIndex].gameObject.SetActive(false);
            _currentTutorialIndex++;
            _tutorials[_currentTutorialIndex].gameObject.SetActive(true);
        }
        else
        {
            _tutorials[_currentTutorialIndex].gameObject.SetActive(false);
            PlayerPrefs.SetInt(TurorialShowed, 1);
            Time.timeScale = 1f;
            _player.GetComponent<PlayerKickingBall>().enabled = true;
            _player.GetComponent<PlayerMovement>().enabled = true;
            PlayerPrefs.Save();
        }
    }

    private void ShowTutorial()
    {
        if (_showTutorial && _currentTutorialIndex < _tutorials.Count)
        {
            _tutorials[_currentTutorialIndex].gameObject.SetActive(true);
            Time.timeScale = 0f;
            _player.GetComponent<PlayerKickingBall>().enabled = false;
            _player.GetComponent<PlayerMovement>().enabled = false;
        }
    }
}

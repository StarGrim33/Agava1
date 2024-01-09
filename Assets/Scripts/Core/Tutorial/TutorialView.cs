using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class TutorialView : MonoBehaviour
    {
        [SerializeField] private List<Button> _tutorials;
        [SerializeField] private GameObject _player;

        private TutorialPresenter _presenter;

        public int TutorialsCount => _tutorials.Count;

        private void Awake()
        {
            _presenter = new TutorialPresenter(this);
        }

        public void OnTutorialClick()
        {
            _presenter.OnTutorialClick();
        }

        public void HideTutorial(TutorialModel tutorialModel)
        {
            if (tutorialModel.CurrentTutorialIndex < TutorialsCount)
            {
                _tutorials[tutorialModel.CurrentTutorialIndex].gameObject.SetActive(false);
            }
        }

        public void ShowTutorial(TutorialModel tutorialModel)
        {
            if (tutorialModel.ShowTutorial && tutorialModel.CurrentTutorialIndex < TutorialsCount)
            {
                _tutorials[tutorialModel.CurrentTutorialIndex].gameObject.SetActive(true);
                Time.timeScale = 0f;
                _player.GetComponent<PlayerKickingBall>().enabled = false;
                _player.GetComponent<PlayerMovement>().enabled = false;
            }
        }

        public void EnablePlayerControls()
        {
            Time.timeScale = 1f;
            _player.GetComponent<PlayerKickingBall>().enabled = true;
            _player.GetComponent<PlayerMovement>().enabled = true;
        }
    }
}

using Agava.YandexGames;
using Lean.Localization;
using Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class LeaderboardOpener : MonoBehaviour
    {
        [SerializeField] private GameObject _leaderboardPanel;
        [SerializeField] private GameObject _notAuthorizedPanel;
        [SerializeField] private Button _openLeaderboardButton;
        [SerializeField] private Button _authorizeButton;
        [SerializeField] private Button _declineAuthorizeButton;
        [SerializeField] private TMP_Text[] _playersName;
        [SerializeField] private TMP_Text[] _playersScore;
        [SerializeField] private TextOptimizer _textOptimizer;

        public void OnOpenLeaderBoard()
        {
            if (PlayerAccount.IsAuthorized)
            {
                OnGetLeaderboardEntriesButtonClick();
                _leaderboardPanel.SetActive(true);
            }
            else
            {
                PlayerAccount.Authorize();
                _notAuthorizedPanel.gameObject.SetActive(true);
            }
        }

        public void OnGetLeaderboardEntriesButtonClick()
        {
            Leaderboard.GetEntries(Constants.LeaderboardName, (result) =>
            {
                ClearLeaderboardPanel();

                for (int i = 0; i < result.entries.Length; i++)
                {
                    string name = result.entries[i].player.publicName;
                    int score = result.entries[i].score;
                    int rank = result.entries[i].rank;

                    if (string.IsNullOrEmpty(name))
                    {
                        if (LeanLocalization.GetFirstCurrentLanguage() == Constants.RussianCode)
                            name = Constants.RussianAnonimName;
                        else if (LeanLocalization.GetFirstCurrentLanguage() == Constants.EnglishCode)
                            name = Constants.EnglishAnonimName;
                        else if (LeanLocalization.GetFirstCurrentLanguage() == Constants.TurkishCode)
                            name = Constants.EnglishAnonimName;
                        else
                            name = Constants.RussianAnonimName;
                    }

                    _playersName[i].text = _textOptimizer.Optimize(name);
                    _playersScore[i].text = $"{score}";
                }
            });
        }

        private void ClearLeaderboardPanel()
        {
            foreach (var text in _playersName)
            {
                text.text = string.Empty;
            }

            foreach (var text in _playersScore)
            {
                text.text = string.Empty;
            }
        }
    }
}

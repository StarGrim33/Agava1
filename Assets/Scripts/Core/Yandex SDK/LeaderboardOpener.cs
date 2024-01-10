using Agava.YandexGames;
using Interfaces;
using Lean.Localization;
using TMPro;
using UnityEngine;
using Utils;

namespace Core
{
    public class LeaderboardOpener : MonoBehaviour, IAuthorize
    {
        [SerializeField] private GameObject _leaderboardPanel;
        [SerializeField] private GameObject _notAuthorizedPanel;
        [SerializeField] private TMP_Text[] _playersName;
        [SerializeField] private TMP_Text[] _playersScore;
        [SerializeField] private TextOptimizer _textOptimizer;

        public void OnOpenLeaderBoard()
        {
            switch (PlayerAccount.IsAuthorized)
            {
                case true:
                    ShowLeaderboard();
                    break;

                case false:
                    Authorize();
                    break;
            }
        }

        public void Authorize()
        {
            PlayerAccount.Authorize();
            _notAuthorizedPanel.gameObject.SetActive(true);
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
                        name = LeanLocalization.GetFirstCurrentLanguage() switch
                        {
                            Constants.RussianCode => Constants.RussianAnonimName,
                            Constants.EnglishCode => Constants.EnglishAnonimName,
                            Constants.TurkishCode => Constants.EnglishAnonimName,
                            _ => Constants.RussianAnonimName,
                        };
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

        private void ShowLeaderboard()
        {
            OnGetLeaderboardEntriesButtonClick();
            _leaderboardPanel.SetActive(true);
        }
    }
}

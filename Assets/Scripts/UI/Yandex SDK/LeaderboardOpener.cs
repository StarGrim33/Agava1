using Agava.YandexGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardOpener : MonoBehaviour
{
    private const string AnonymousName = "Anonymous";
    private const string LeaderboardName = "1";

    [SerializeField] private GameObject _leaderboardPanel;
    [SerializeField] private GameObject _notAuthorizedPanel;
    [SerializeField] private Button _openLeaderboardButton;
    [SerializeField] private Button _authorizeButton;
    [SerializeField] private Button _declineAuthorizeButton;
    [SerializeField] private TMP_Text[] _playersName;
    [SerializeField] private TMP_Text[] _playersScore;

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
        Agava.YandexGames.Leaderboard.GetEntries(LeaderboardName, (result) =>
        {
            Debug.Log($"My rank = {result.userRank}");
            ClearLeaderboardPanel();

            for (int i = 0; i < result.entries.Length; i++)
            {
                string name = result.entries[i].player.publicName;
                int score = result.entries[i].score;
                int rank = result.entries[i].rank;

                if (string.IsNullOrEmpty(name))
                    name = AnonymousName;

                _playersName[i].text = $"{name}";
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

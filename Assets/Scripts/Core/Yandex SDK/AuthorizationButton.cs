using Agava.YandexGames;
using UnityEngine;
using UnityEngine.UI;

public class AuthorizationButton : MonoBehaviour, IAuthorize
{
    [SerializeField] private Button _button;

    public void OnAuthorizeButtonClick()
    {
        Authorize();
    }

    public void OnDeclineAuthorizationButton(GameObject panel)
    {
        panel.gameObject.SetActive(false);
    }

    public void Authorize()
    {
        PlayerAccount.Authorize();
    }
}

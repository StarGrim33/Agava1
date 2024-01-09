using Agava.YandexGames;
using Interfaces;
using UnityEngine;

namespace Core
{
    public class AuthorizationButton : MonoBehaviour, IAuthorize
    {
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
}

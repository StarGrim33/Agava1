using Agava.YandexGames;
using UnityEngine;

public class Authorization : MonoBehaviour, IAuthorize
{
    private void Start()
    {
        Authorize();
    }

    public void Authorize()
    {
        PlayerAccount.Authorize();
    }
}

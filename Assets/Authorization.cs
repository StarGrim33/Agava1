using Agava.YandexGames;
using UnityEngine;

public class Authorization : MonoBehaviour
{
    private void Start()
    {
        PlayerAccount.Authorize();
    }
}

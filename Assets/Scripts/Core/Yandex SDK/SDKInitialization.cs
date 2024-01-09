using System.Collections;
using Agava.YandexGames;
using UnityEngine;

namespace Core
{
    public class SDKInitialization : MonoBehaviour
    {
        private void Awake()
        {
            YandexGamesSdk.CallbackLogging = true;
        }

        private IEnumerator Start()
        {
            yield return YandexGamesSdk.Initialize();

            if (YandexGamesSdk.IsInitialized)
                InterstitialAd.Show();
        }
    }
}

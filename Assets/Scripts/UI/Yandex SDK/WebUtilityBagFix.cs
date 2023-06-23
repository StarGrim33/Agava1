using Agava.WebUtility;
using Agava.YandexGames;
using Lean.Localization;
using System.Collections;
using UnityEngine;

public class WebUtilityBagFix : MonoBehaviour
{
    private void OnEnable()
    {
        WebApplication.InBackgroundChangeEvent += OnInBackgroundChange;
    }

    private void OnDisable()
    {
        WebApplication.InBackgroundChangeEvent -= OnInBackgroundChange;
    }

    private void OnInBackgroundChange(bool inBackground)
    {
        AudioListener.pause = inBackground;
        AudioListener.volume = inBackground ? 0f : 1f;
    }
}

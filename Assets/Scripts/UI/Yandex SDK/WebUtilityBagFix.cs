using Agava.WebUtility;
using Agava.YandexGames;
using Lean.Localization;
using UnityEngine;

public class WebUtilityBagFix : MonoBehaviour
{
    [SerializeField] private LeanLocalization _localization;

    private void OnEnable()
    {
        WebApplication.InBackgroundChangeEvent += OnInBackgroundChange;
        string language = YandexGamesSdk.Environment.i18n.lang;

        _localization.CurrentLanguage = language switch
        {
            "tr" => "Turkey",
            "ru" => "Russian",
            "en" => "English",
            _ => "Russian",
        };
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

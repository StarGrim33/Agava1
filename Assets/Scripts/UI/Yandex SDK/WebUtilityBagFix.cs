using Agava.WebUtility;
using Agava.YandexGames;
using Lean.Localization;
using UnityEngine;

public class WebUtilityBagFix : MonoBehaviour
{
    private const string EnglishCode = "en";
    private const string RussianCode = "ru";
    private const string TurkishCode = "tr";

    [SerializeField] private LeanLocalization _localization;

    private void Awake()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
            ChangeLanguage();
#endif
    }

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

    private void ChangeLanguage()
    {
        string languageCode = YandexGamesSdk.Environment.i18n.lang;

        string language = languageCode switch
        {
            EnglishCode => "English",
            RussianCode => "Russian",
            TurkishCode => "Turkey",
            _ => "Russian"
        };

        LeanLocalization.SetCurrentLanguageAll(language);
    }
}

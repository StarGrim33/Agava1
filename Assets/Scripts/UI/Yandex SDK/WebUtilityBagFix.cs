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

        switch(language)
        {
            case "tr":
                _localization.CurrentLanguage = "Turkey";
                break;
            case "ru":
                _localization.CurrentLanguage = "Russian";
                break;
            case "en":
                _localization.CurrentLanguage = "English";
                break;
        }
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
